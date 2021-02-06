using IntelligentHabitacion.App.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class RatingCleaningViewModel : BaseViewModel
    {
        public ICommand OnConcludeCommand { protected set; get; }
        public RatingCleaningModel Model { get; set; }

        public RatingCleaningViewModel()
        {
            OnConcludeCommand = new Command(async () => await OnConclude());

            Model = new RatingCleaningModel
            {
                Date = DateTime.Today,
                Name = "Welisson Arley",
                RatingStars = 2,
                Room = "Banheiro"
            };
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                var x = Model;

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
