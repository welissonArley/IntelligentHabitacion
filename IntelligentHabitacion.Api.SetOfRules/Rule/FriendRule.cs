using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.SetOfRules.Token;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System;
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

        public Tuple<string, string> GetCodeToAddFriend(string userToken)
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

            return Tuple.Create(codeRandom, user.EncryptedId());
        }

        public List<ResponseFriendJson> GetFriends()
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeAssociationId == null)
                throw new UserIsNotPartOfAHomeException();

            var mapper = new Mapper.Mapper();
            return loggedUser.HomeAssociation.Users.Where(c => c.Id != loggedUser.Id).Select(c => mapper.MapperModelToJsonFriend(c)).ToList();
        }
    }
}
