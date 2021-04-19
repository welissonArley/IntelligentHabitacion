﻿using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Communication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.EditTaskAssign
{
    public class EditTaskAssignUseCase : IEditTaskAssignUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;
        private readonly ICleaningScheduleReadOnlyRepository _cleaningScheduleReadOnlyRepository;
        private readonly ICleaningScheduleWriteOnlyRepository _cleaningScheduleWriteOnlyRepository;

        public EditTaskAssignUseCase(ILoggedUser loggedUser, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            IUnitOfWork unitOfWork, IHashids hashids, IUserReadOnlyRepository userReadOnlyRepository,
            ICleaningScheduleReadOnlyRepository cleaningScheduleReadOnlyRepository,
            IPushNotificationService pushNotificationService,
            ICleaningScheduleWriteOnlyRepository cleaningScheduleWriteOnlyRepository)
        {
            _hashids = hashids;
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _cleaningScheduleReadOnlyRepository = cleaningScheduleReadOnlyRepository;
            _pushNotificationService = pushNotificationService;
            _cleaningScheduleWriteOnlyRepository = cleaningScheduleWriteOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(RequestEditAssignCleaningScheduleJson request)
        {
            var loggedUser = await _loggedUser.User();
            
            var scheduleRoom = await _cleaningScheduleReadOnlyRepository.GetScheduleRoomForCurrentMonth(loggedUser.HomeAssociation.HomeId, request.Room);

            var usersIds = request.UserIds.Select(c => _hashids.DecodeLong(c).First());

            var scheduleToRemoveOrFinish = scheduleRoom.Where(c => usersIds.All(w => w != c.UserId));

            await FinishOrRemoveSchedule(scheduleToRemoveOrFinish);

            var scheduleToAdd = usersIds.Where(c => scheduleRoom.All(w => w.UserId != c));

            await Add(scheduleToAdd, request.Room, loggedUser.HomeAssociation.HomeId);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);
            await SendNotification(friends.Where(c => c.Id != loggedUser.Id && usersIds.Any(w => w == c.Id)).Select(c => c.PushNotificationId).ToList());

            return response;
        }

        private async Task FinishOrRemoveSchedule(IEnumerable<Domain.Entity.CleaningSchedule> list)
        {
            foreach(var schedule in list)
            {
                if (schedule.CleaningTasksCompleteds.Any())
                    await _cleaningScheduleWriteOnlyRepository.FinishTask(schedule.Id);
                else
                    _cleaningScheduleWriteOnlyRepository.Remove(schedule);
            }
        }
        private async Task Add(IEnumerable<long> list, string room, long homeId)
        {
            await _cleaningScheduleWriteOnlyRepository.Add(list.Select(c => new Domain.Entity.CleaningSchedule
            {
                HomeId = homeId,
                ScheduleStartAt = DateTime.UtcNow,
                UserId = c,
                Room = room
            }));
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning Schedule updated 🏡" },
                { "pt", "Cronograma de limpeza atualizado 🏡" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "Enter in the app and check" },
                { "pt", "Entre no app e confira ;)" }
            };

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
