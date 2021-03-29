using AutoMapper;
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

        public GetTasksUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
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
                        Friends = _mapper.Map<List<ResponseFriendSimplifiedJson>>(usersAtHome),
                        Rooms = _mapper.Map<List<ResponseRoomJson>>(loggedUser.HomeAssociation.Home.Rooms)
                    }
                });
            }

            return null;
        }

        private async Task<ResponseOutput> CreateResponse(Domain.Entity.User loggedUser, object json)
        {
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
