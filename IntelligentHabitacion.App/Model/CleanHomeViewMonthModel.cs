using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.App.Model
{
    public class CleanHomeViewMonthModel
    {
        public DateTime Month { get; set; }
        public List<FriendSchedule> FriendSchedules { get; set; }
    }

    public class FriendSchedule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor { get; set; }
        public string Room { get; set; }
        public List<int> DaysOfTheMonthTheFriendCleanedTheRoom { get; set; }
    }
}
