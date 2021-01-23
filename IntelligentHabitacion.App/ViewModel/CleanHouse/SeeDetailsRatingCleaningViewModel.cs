using IntelligentHabitacion.App.Model;
using System;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class SeeDetailsRatingCleaningViewModel : BaseViewModel
    {
        public ObservableCollection<RatingCleaningModel> ModelList { get; set; }

        public SeeDetailsRatingCleaningViewModel()
        {
            ModelList = new ObservableCollection<RatingCleaningModel>
            {
                new RatingCleaningModel
                {
                    Date = DateTime.UtcNow.AddDays(-1),
                    Name = "Welisson",
                    Room = "Banheiro",
                    RatingStars = 3,
                    Comment = "Faltou o armario"
                },
                new RatingCleaningModel
                {
                    Date = DateTime.UtcNow.AddDays(-1),
                    Name = "Matheus",
                    Room = "Área de serviço",
                    RatingStars = 4,
                    Comment = "muito bom"
                },
                new RatingCleaningModel
                {
                    Date = DateTime.UtcNow.AddDays(-1),
                    Name = "William",
                    Room = "Cozinha",
                    RatingStars = 5
                }
            };
        }
    }
}
