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
                    Name = "Welisson",
                    Date = DateTime.Today,
                    Feedback = "Eu gostei",
                    RatingStars = 5,
                    Room = "Escritório"
                },
                new RatingCleaningModel
                {
                    Name = "Welisson",
                    Date = DateTime.Today,
                    Feedback = "Ok",
                    RatingStars = 4,
                    Room = "Escritório"
                },
                new RatingCleaningModel
                {
                    Name = "Welisson",
                    Date = DateTime.Today,
                    Feedback = "Ficou bom",
                    RatingStars = 3,
                    Room = "Escritório"
                },
                new RatingCleaningModel
                {
                    Name = "Welisson",
                    Date = DateTime.Today,
                    Feedback = "Faltou as janelas",
                    RatingStars = 2,
                    Room = "Escritório"
                }
            };
        }
    }
}
