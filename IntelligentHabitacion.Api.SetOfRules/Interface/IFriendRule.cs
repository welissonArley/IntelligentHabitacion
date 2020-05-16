using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IFriendRule
    {
        List<ResponseFriendJson> GetFriends();
        ResponseCodeToAddFriendJson GetCodeToAddFriend(string userToken);
        ResponseCodeWasReadJson CodeWasRead(string userToken, string code);
    }
}
