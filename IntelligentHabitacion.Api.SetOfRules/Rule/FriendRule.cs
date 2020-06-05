using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserRepository _userRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public FriendRule(ILoggedUser loggedUser, IUserRepository userRepository, IPushNotificationService pushNotificationService)
        {
            _loggedUser = loggedUser;
            _userRepository = userRepository;
            _pushNotificationService = pushNotificationService;
        }

        public List<ResponseFriendJson> GetFriends()
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeAssociationId == null)
                throw new UserIsNotPartOfAHomeException();

            var mapper = new Mapper.Mapper();

            var friends = _userRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

            return friends.Where(c => c.Id != loggedUser.Id).Select(c => mapper.MapperModelToJsonFriend(c, loggedUser.HomeAssociation.JoinedOn)).ToList();
        }

        public ResponseFriendJson ChangeDateJoinHome(RequestChangeDateJoinHomeJson request)
        {
            var loggedUser = _loggedUser.User();
            var friend = _userRepository.GetById(new User().DecryptedId(request.FriendId));

            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.Home.AdministratorId != loggedUser.Id)
                throw new YouCannotPerformThisActionException();

            friend.HomeAssociation.JoinedOn = request.JoinOn;

            var response = new Mapper.Mapper().MapperModelToJsonFriend(friend, loggedUser.HomeAssociation.JoinedOn);

            _userRepository.Update(friend);

            return response;
        }

        public void NotifyOrderHasArrived(string friendId)
        {
            var loggedUser = _loggedUser.User();
            var friend = _userRepository.GetById(new User().DecryptedId(friendId));

            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            var titles = new Dictionary<string, string>
            {
                { "en", "Delivery received 📬" },
                { "pt", "Encomenda recebida 📬" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "You have received an order and it is waiting for you 😃" },
                { "pt", "Você recebeu uma encomenda e ela está te esperando 😃" }
            };
            var data = new Dictionary<string, string> { { "ho", "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { friend.PushNotificationId }, data);
        }
    }
}
