using System;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestChangeDateJoinHomeJson
    {
        public string FriendId { get; set; }
        public DateTime JoinOn { get; set; }
    }
}
