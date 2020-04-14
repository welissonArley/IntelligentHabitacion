using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class FriendRule : IFriendRule
    {
        private readonly ILoggedUser _loggedUser;

        public FriendRule(ILoggedUser loggedUser)
        {
            _loggedUser = loggedUser;
        }

        public List<ResponseFriendJson> GetFriends()
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeId == null)
                throw new UserIsNotPartOfAHomeException();

            var mapper = new Mapper.Mapper();
            return loggedUser.Home.Users.Where(c => c.Id != loggedUser.Id).Select(c => mapper.MapperModelToJsonFriend(c)).ToList();
        }
    }
}
