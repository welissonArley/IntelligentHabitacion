using IntelligentHabitacion.App.Model;
using System;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class RatingCleaningViewModel : BaseViewModel
    {
        public RatingCleaningModel Model { get; set; }

        public RatingCleaningViewModel()
        {
            Model = new RatingCleaningModel
            {
                Date = DateTime.UtcNow.AddDays(-1),
                Name = "Matheus",
                Room = "Banheiro",
                RatingStars = 0
            };
        }
    }
}
