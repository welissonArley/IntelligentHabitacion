using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Useful;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserRepository _userRepository;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ICodeRepository _codeRepository;
        private readonly IEmailHelper _emailHelper;
        private readonly ICryptographyPassword _cryptography;

        public FriendRule(ILoggedUser loggedUser, IUserRepository userRepository,
            IPushNotificationService pushNotificationService, ICodeRepository codeRepository,
            IEmailHelper emailHelper, ICryptographyPassword cryptography)
        {
            _cryptography = cryptography;
            _codeRepository = codeRepository;
            _emailHelper = emailHelper;
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
                { "en", "You have received an order and it is waiting for you ✈️" },
                { "pt", "Você recebeu uma encomenda e ela está te esperando ✈️" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.OrderReceived, "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { friend.PushNotificationId }, data);
        }

        public void ChangeAdministrator(RequestAdminActionsOnFriendJson request)
        {
            var friendIdDecrypted = new User().DecryptedId(request.FriendId);
            var friend = _userRepository.GetById(friendIdDecrypted);

            ValidateActionOnFriend(friend, request, CodeType.ChangeAdministrator);

            friend.HomeAssociation.Home.AdministratorId = friendIdDecrypted;
            var pushNotificationId = friend.PushNotificationId;
            _userRepository.Update(friend);

            var titles = new Dictionary<string, string>
            {
                { "en", "Congratulations new Admin 🦁" },
                { "pt", "Parabéns novo Admin 🦁" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "You are now the new administrator. Good luck 🏁" },
                { "pt", "Você agora é o novo Administrador. Boa sorte 🏁" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.NewAdmin, "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }

        public void RequestCodeChangeAdministrator()
        {
            var loggedUser = _loggedUser.User();
            var codeRandom = CreateCode(CodeType.ChangeAdministrator, loggedUser.Id);

            _emailHelper.ChangeAdmin(loggedUser.Email, codeRandom, loggedUser.Name);
        }

        public void RequestCodeRemoveFriend()
        {
            var loggedUser = _loggedUser.User();
            var codeRandom = CreateCode(CodeType.RemoveFriend, loggedUser.Id);

            _emailHelper.RemoveFriend(loggedUser.Email, codeRandom, loggedUser.Name);
        }

        public void RemoveFriend(RequestAdminActionsOnFriendJson request)
        {
            var friendIdDecrypted = new User().DecryptedId(request.FriendId);
            var friend = _userRepository.GetById(friendIdDecrypted);

            ValidateActionOnFriend(friend, request, CodeType.RemoveFriend);

            friend.HomeAssociationId = null;
            friend.HomeAssociation.ExitOn = DateTimeController.DateTimeNow();
            var pushNotificationId = friend.PushNotificationId;

            _userRepository.Update(friend);

            var titles = new Dictionary<string, string>
            {
                { "en", "You have been removed from Home" },
                { "pt", "Você foi removido do Lar" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "Good luck with your new journey 🚀" },
                { "pt", "Boa sorte com sua nova jornada 🚀" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.RemovedFromHome, "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }

        private string CreateCode(CodeType codeType, long userId)
        {
            var codeRandom = new CodeGenerator().Random6Chars();

            var userCodes = _codeRepository.GetByUser(userId);
            foreach (var code in userCodes)
                _codeRepository.DeleteOnDatabase(code);

            _codeRepository.Create(new Code
            {
                Active = true,
                Type = codeType,
                CreateDate = DateTimeController.DateTimeNow(),
                Value = codeRandom,
                UserId = userId
            });

            return codeRandom;
        }
        private void ValidateActionOnFriend(User friend, RequestAdminActionsOnFriendJson request, CodeType codeType)
        {
            var loggedUser = _loggedUser.User();

            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (!loggedUser.Password.Equals(_cryptography.Encrypt(request.Password)))
                throw new CodeOrPasswordInvalidException();

            var userCode = codeType == CodeType.ChangeAdministrator ? _codeRepository.GetByUserChangeAdministrator(loggedUser.Id) : _codeRepository.GetByUserRemoveFriend(loggedUser.Id);

            if (userCode == null || !userCode.Value.Equals(request.Code.ToUpper()))
                throw new CodeOrPasswordInvalidException();

            if (userCode.CreateDate.AddMinutes(10) < DateTimeController.DateTimeNow())
                throw new ExpiredCodeException();

            _codeRepository.DeleteOnDatabase(userCode);
        }
    }
}
