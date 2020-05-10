using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IFriendRule
    {
        List<ResponseFriendJson> GetFriends();
        Tuple<string, string> GetCodeToAddFriend(string userToken);
    }
}
