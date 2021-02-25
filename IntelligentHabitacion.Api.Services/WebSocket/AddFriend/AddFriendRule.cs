using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System.Linq;

namespace IntelligentHabitacion.Api.Services.WebSocket.AddFriend
{
    public class AddFriendRule : IAddFriendRule
    {
        private readonly ITokenController _tokenController;
        private readonly ICodeRepository _codeRepository;
        private readonly IUserRepository _userRepository;

        public AddFriendRule(ICodeRepository codeRepository, IUserRepository userRepository, ITokenController tokenController)
        {
            _tokenController = tokenController;
            _codeRepository = codeRepository;
            _userRepository = userRepository;
        }

        public ResponseCodeWasReadJson CodeWasRead(string userToken, string code)
        {
            var email = _tokenController.User(userToken);
            var user = _userRepository.GetByEmail(email);

            if (user.HomeAssociationId != null)
                throw new IntelligentHabitacionException(ResourceTextException.USER_IS_PART_OF_A_HOME);

            var codeResult = _codeRepository.GetByCode(code);
            if (codeResult == null)
                throw new IntelligentHabitacionException(ResourceTextException.CODE_INVALID);

            var admin = _userRepository.GetById(codeResult.UserId);

            _codeRepository.DeleteOnDatabase(codeResult);

            return new ResponseCodeWasReadJson
            {
                Id = user.EncryptedId(),
                Name = user.Name,
                Phonenumbers = user.Phonenumbers.Select(c => new ResponsePhonenumberJson { Number = c.Number }).ToList(),
                EmergencyContacts = user.EmergecyContacts.Select(c => new ResponseEmergencyContactJson
                { Name = c.Name, Relationship = c.Relationship, Phonenumber = c.Phonenumber }).ToList(),
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
    }
}
