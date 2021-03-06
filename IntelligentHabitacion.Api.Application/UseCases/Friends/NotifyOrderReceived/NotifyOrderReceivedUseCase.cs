using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Enums;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Exception.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.NotifyOrderReceived
{
    public class NotifyOrderReceivedUseCase : INotifyOrderReceivedUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserReadOnlyRepository _repository;

        public NotifyOrderReceivedUseCase(ILoggedUser loggedUser, IPushNotificationService pushNotificationService,
            IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            IUserReadOnlyRepository repository)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _repository = repository;
        }

        public async Task<ResponseOutput> Execute(long friendId)
        {
            var loggedUser = await _loggedUser.User();
            var friend = await _repository.GetById(friendId);

            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            SendNotification(friend.PushNotificationId);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void SendNotification(string pushNotificationId)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Delivery received 📬" },
                { "pt", "Encomenda recebida 📬" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "You have received an order and it is waiting for you ✈️" },
                { "pt", "Você recebeu uma encomenda e ela está te esperando ✈️" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.OrderReceived, "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }
    }
}
