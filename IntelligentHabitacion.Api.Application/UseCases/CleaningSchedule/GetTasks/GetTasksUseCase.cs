﻿using AutoMapper;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTasks
{
    public class GetTasksUseCase : IGetTasksUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IHashids _hashids;

        public GetTasksUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper, IHashids hashids,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(DateTime date)
        {
            var loggedUser = await _loggedUser.User();

            if (!loggedUser.HomeAssociation.Home.Rooms.Any())
            {
                return await CreateResponse(loggedUser, new ResponseTasksJson
                {
                    Action = Communication.Enums.NeedAction.RegisterRoom,
                    Message = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId ? ResourceText.MESSAGE_REGISTER_ROOM_ADMIN : string.Format(ResourceText.MESSAGE_REGISTER_ROOM, (await _userReadOnlyRepository.GetById(loggedUser.HomeAssociation.Home.AdministratorId)).Name)
                });
            }

            var homeHasCleaningSchedule = await _repository.HomeHasCleaningScheduleCreated(loggedUser.HomeAssociation.HomeId);

            if (!homeHasCleaningSchedule)
            {
                var usersAtHome = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

                return await CreateResponse(loggedUser, new ResponseTasksJson
                {
                    Action = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId ? Communication.Enums.NeedAction.CreateTheCleaningSchedule : Communication.Enums.NeedAction.InformationCreateCleaningSchedule,
                    Message = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId ? "" : string.Format(ResourceText.DESCRIPTION_CREATE_CLEANING_SCHEDULE, usersAtHome.First(c => c.Id == loggedUser.HomeAssociation.Home.AdministratorId).Name),
                    CreateSchedule = new ResponseCreateScheduleCleaningHouseJson
                    {
                        Friends = _mapper.Map<List<ResponseUserSimplifiedJson>>(usersAtHome),
                        Rooms = _mapper.Map<List<ResponseRoomJson>>(loggedUser.HomeAssociation.Home.Rooms)
                    }
                });
            }

            var schedule = await _repository.GetTasksForTheMonth(date, loggedUser.HomeAssociation.HomeId);
            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

            return await CreateResponse(loggedUser, new ResponseTasksJson
            {
                Action = Communication.Enums.NeedAction.None,
                Schedule = await Mapper(date, loggedUser, friends, schedule)
            });
        }

        private async Task<ResponseScheduleTasksCleaningHouseJson> Mapper(DateTime date, Domain.Entity.User loggedUser,
            IList<Domain.Entity.User> users, List<Domain.Entity.CleaningSchedule> schedules)
        {
            return new ResponseScheduleTasksCleaningHouseJson
            {
                Date = date,
                ProfileColor = loggedUser.ProfileColor,
                Name = loggedUser.Name,
                AmountOfTasks = schedules.Count(c => c.UserId == loggedUser.Id),
                Tasks = await ScheduleTasksFormatter(date, loggedUser, users, schedules)
            };
        }
        public async Task<List<ResponseTaskJson>> ScheduleTasksFormatter(DateTime date, Domain.Entity.User loggedUser,
            IList<Domain.Entity.User> users, IList<Domain.Entity.CleaningSchedule> schedules)
        {
            var today = DateTime.UtcNow;
            var response = new List<ResponseTaskJson>();

            var myRooms = schedules.Where(c => c.UserId == loggedUser.Id).Select(c => c.Room).Distinct().OrderBy(c => c);
            var otherRooms = schedules.Where(c => !myRooms.Contains(c.Room)).Select(c => c.Room).Distinct().OrderBy(c => c);

            foreach (var room in myRooms)
            {
                var task = schedules.First(c => c.UserId == loggedUser.Id && c.Room.Equals(room));
                var canCompletedToday = await _repository.TaskCleanedOnDate(task.Id, date);

                var schedule = new ResponseTaskJson
                {
                    IdTaskToRegisterRoomCleaning = _hashids.EncodeLong(task.Id),
                    CanEdit = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId,
                    CanRate = await _repository.ThereAreaTaskToUserRateThisMonth(loggedUser.Id, room),
                    CanCompletedToday = !canCompletedToday && date.Month == today.Month && date.Year == today.Year,
                    Room = room,
                    Assign = new List<ResponseUserSimplifiedJson>
                    {
                        new ResponseUserSimplifiedJson
                        {
                            Id = _hashids.EncodeLong(loggedUser.Id),
                            Name = loggedUser.Name,
                            ProfileColor = loggedUser.ProfileColor
                        }
                    }
                };

                var otherUsers = users.Where(w =>
                            schedules.Where(c => c.Room.Equals(room) && c.UserId != loggedUser.Id)
                            .Select(c => c.UserId).Contains(w.Id)).ToList();

                foreach (var user in otherUsers)
                {
                    schedule.Assign.Add(new ResponseUserSimplifiedJson
                    {
                        Id = _hashids.EncodeLong(user.Id),
                        Name = user.Name,
                        ProfileColor = user.ProfileColor
                    });
                }

                response.Add(schedule);
            }

            foreach (var room in otherRooms)
            {
                response.Add(new ResponseTaskJson
                {
                    CanEdit = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId,
                    CanRate = await _repository.ThereAreaTaskToUserRateThisMonth(loggedUser.Id, room),
                    CanCompletedToday = false,
                    Room = room,
                    Assign = users
                        .Where(w => schedules.Where(c => c.Room.Equals(room)).Select(c => c.UserId).Contains(w.Id))
                        .OrderBy(c => c.Name).Select(c => new ResponseUserSimplifiedJson
                        {
                            Id = _hashids.EncodeLong(c.Id),
                            Name = c.Name,
                            ProfileColor = c.ProfileColor
                        }).ToList()
                });
            }

            var roomsWithoutAssign = loggedUser.HomeAssociation.Home.Rooms
                .Where(c => !myRooms.Contains(c.Name) && !otherRooms.Contains(c.Name))
                .Select(c => c.Name).OrderBy(c => c);

            foreach (var room in roomsWithoutAssign)
            {
                response.Add(new ResponseTaskJson
                {
                    CanEdit = loggedUser.Id == loggedUser.HomeAssociation.Home.AdministratorId,
                    CanRate = false,
                    CanCompletedToday = false,
                    Room = room
                });
            }
            
            return response;
        }

        private async Task<ResponseOutput> CreateResponse(Domain.Entity.User loggedUser, object json)
        {
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
