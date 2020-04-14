using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IFriendRule
    {
        List<ResponseFriendJson> GetFriends();
    }
}
