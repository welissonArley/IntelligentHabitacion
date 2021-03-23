using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class RatingCleaningViewModel : BaseViewModel
    {
        /*private readonly ICleaningScheduleRule _rule;

        public ICommand OnConcludeCommand { protected set; get; }
        public ICommand CallbackOnConcludeCommand { set; get; }
        public RatingCleaningModel Model { get; set; }

        public RatingCleaningViewModel(ICleaningScheduleRule rule)
        {
            _rule = rule;

            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                var averageRating = await _rule.RateFriendTask(Model);

                CallbackOnConcludeCommand?.Execute(new TaskForTheMonthDetails
                {
                    CanBeRate = false,
                    RatingStars = averageRating,
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
        }*/
    }
}
