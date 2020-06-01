using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.SetOfRules.Token;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ITokenController _tokenController;
        private readonly IUserRepository _userRepository;
        private readonly ICodeRepository _codeRepository;

        public FriendRule(ILoggedUser loggedUser, ITokenController tokenController, IUserRepository userRepository,
            ICodeRepository codeRepository)
        {
            _loggedUser = loggedUser;
            _tokenController = tokenController;
            _userRepository = userRepository;
            _codeRepository = codeRepository;
        }

        public ResponseCodeWasReadJson CodeWasRead(string userToken, string code)
        {
            var email = _tokenController.User(userToken);
            var user = _userRepository.GetByEmail(email);

            if(user.HomeAssociationId != null)
                throw new IntelligentHabitacionException(ResourceTextException.USER_IS_PART_OF_A_HOME);

            var codeResult = _codeRepository.GetByCode(code);
            if(codeResult == null)
                throw new IntelligentHabitacionException(ResourceTextException.CODE_INVALID);

            var admin = _userRepository.GetById(codeResult.UserId);

            _codeRepository.DeleteOnDatabase(codeResult);

            var mapper = new Mapper.Mapper();
            return new ResponseCodeWasReadJson
            {
                Id = user.EncryptedId(),
                Name = user.Name,
                Phonenumbers = user.Phonenumbers.Select(c => mapper.MapperModelToJson(c)).ToList(),
                EmergencyContact = user.EmergecyContacts.Select(c => mapper.MapperModelToJson(c)).ToList(),
                ProfileColor = user.ProfileColor,
                AdminId = admin.EncryptedId()
            };
        }

        public ResponseCodeToAddFriendJson GetCodeToAddFriend(string userToken)
        {
            var email = _tokenController.User(userToken);
            var user = _userRepository.GetByEmail(email);
            if (user == null || user.HomeAssociationId == null || user.HomeAssociation.Home.AdministratorId != user.Id)
                throw new IntelligentHabitacionException(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

            var codeRandom = new CodeGenerator().Random36Chars();

            var userCodes = _codeRepository.GetByUser(user.Id);
            foreach (var code in userCodes)
                _codeRepository.DeleteOnDatabase(code);

            _codeRepository.Create(new Code
            {
                Active = true,
                Type = CodeType.AddFriend,
                CreateDate = DateTimeController.DateTimeNow(),
                Value = codeRandom,
                UserId = user.Id
            });

            return new ResponseCodeToAddFriendJson
            {
                AdminId = user.EncryptedId(),
                Code = codeRandom
            };
        }

        public void ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove)
        {
            if (requestApprove.MonthlyRent <= 0)
                throw new MonthlyRentInvalidException();

            var homeId = _userRepository.GetHomeId(new User().DecryptedId(adminId));
            var friend = _userRepository.GetById(new User().DecryptedId(friendId));
            friend.HomeAssociation = new HomeAssociation
            {
                Active = true,
                CreateDate = DateTimeController.DateTimeNow(),
                HomeId = homeId.Value,
                MonthlyRent = requestApprove.MonthlyRent,
                JoinedOn = requestApprove.JoinedOn
            };
            _userRepository.Update(friend);
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
    }
}
