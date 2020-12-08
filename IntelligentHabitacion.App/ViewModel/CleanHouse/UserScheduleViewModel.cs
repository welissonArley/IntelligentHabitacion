using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using System;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class UserScheduleViewModel : BaseViewModel
    {
        public string ProfileColor { get; set; }
        public ScheduleUserModel Model { get; set; }

        public UserScheduleViewModel(UserPreferences userPreferences)
        {
            ProfileColor = userPreferences.ProfileColor;

            Model = new ScheduleUserModel
            {
                User = "William",
                Month = DateTime.Today,
                Schedules = new System.Collections.Generic.List<ScheduleModel>
                {
                    new ScheduleModel
                    {
                        Date = DateTime.Today,
                        Room = "Área de Serviço",
                        RatingStars = -1,
                        CanBeRate = true
                    },
                    new ScheduleModel
                    {
                        Date = DateTime.Today.AddDays(-5),
                        Room = "Área de Serviço",
                        RatingStars = 4,
                        CanBeRate = false
                    }
                }
            };
        }
    }
}
