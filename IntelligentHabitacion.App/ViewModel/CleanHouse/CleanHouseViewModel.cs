using IntelligentHabitacion.App.Model;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class CleanHouseViewModel : BaseViewModel
    {
        public CleanHomeViewMonthModel Model { get; set; }

        public CleanHouseViewModel()
        {
            Model = new CleanHomeViewMonthModel
            {
                Month = DateTime.UtcNow,
                FriendSchedules = new List<FriendSchedule>
                {
                    new FriendSchedule
                    {
                        Name = "Anilton",
                        ProfileColor = "#657EBF"
                    },
                    new FriendSchedule
                    {
                        Name = "William",
                        Room = "Banheiro",
                        ProfileColor = "#657EBF",
                        DaysOfTheMonthTheFriendCleanedTheRoom = new List<int>{1,2,3,5,7,8,9,10,11,12,13,14,15,16,17,18,19,20, 21, 22, 23, 24, 25}
                    },
                    new FriendSchedule
                    {
                        Name = "Matheus",
                        Room = "Sala",
                        ProfileColor = "#BF658B",
                        DaysOfTheMonthTheFriendCleanedTheRoom = new List<int>{4,5,7}
                    }
                }
            };
        }
    }
}
