﻿using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Reminder
{
    public class ReminderUseCase : IReminderUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;

        public ReminderUseCase(ILoggedUser loggedUser, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            IUnitOfWork unitOfWork, IHashids hashids, IPushNotificationService pushNotificationService,
            IUserReadOnlyRepository userReadOnlyRepository)
        {
            _hashids = hashids;
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(IList<string> usersId)
        {
            var loggedUser = await _loggedUser.User();
            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

            var usersIds = usersId.Distinct()
                   .Select(c => _hashids.DecodeLong(c).First())
                   .Where(c => c != loggedUser.Id);

            await SendNotification(friends.Where(c => usersIds.Contains(c.Id)).Select(c => c.PushNotificationId).ToList());

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning Schedule reminder 😶" },
                { "pt", "Lembrete Cronograma de limpeza 😶" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "Someone asked you to clean the room(s) for which you are responsible 👍" },
                { "pt", "Alguém solicitou que você limpe o(s) cômodos os quais você é responsável 👍" }
            };

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
