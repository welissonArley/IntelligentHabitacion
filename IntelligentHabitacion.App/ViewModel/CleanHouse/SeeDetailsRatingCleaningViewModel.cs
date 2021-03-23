using IntelligentHabitacion.App.Model;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class SeeDetailsRatingCleaningViewModel : BaseViewModel
    {
        public ObservableCollection<RatingCleaningModel> ModelList { get; set; }
    }
}
