using System;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestApproveAddFriendJson
    {
        public DateTime JoinedOn { get; set; }
        public decimal MonthlyRent { get; set; } 
    }
}
