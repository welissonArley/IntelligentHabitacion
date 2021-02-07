using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class RatingCleaningViewModel : BaseViewModel
    {
        public ICommand OnConcludeCommand { protected set; get; }
        public ICommand CallbackOnConcludeCommand { set; get; }
        public RatingCleaningModel Model { get; set; }

        public RatingCleaningViewModel()
        {
            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                CallbackOnConcludeCommand?.Execute(new TaskForTheMonthDetails
                {
                    CanBeRate = false,
                    RatingStars = 5,
                    Room = Model.Room,
                    Id = Model.Id
                });

                HideLoading();

                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
