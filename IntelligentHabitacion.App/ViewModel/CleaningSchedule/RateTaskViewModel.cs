using IntelligentHabitacion.App.Model;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class RateTaskViewModel : BaseViewModel
    {
        public ICommand CallbackOnConcludeCommand { get; set; }
        public ICommand OnConcludeCommand { get; }
        public RateTaskModel Model { get; set; }

        public RateTaskViewModel()
        {
            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                SendingData();

                var averageRating = 3;//await _rule.RateFriendTask(Model);

                await Sucess();
                
                CallbackOnConcludeCommand.Execute(averageRating);

                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public void Initialize(DetailsTaskCleanedOnDayModel task, string room, ICommand callbackOnConcludeCommand)
        {
            CallbackOnConcludeCommand = callbackOnConcludeCommand;
            Model = new RateTaskModel
            {
                TaskId = task.Id,
                Feedback = "",
                RatingStars = 0,
                Name = task.User,
                Room = room,
                Date = task.CleanedAt
            };

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }
    }
}
