using IntelligentHabitacion.ViewModel.RegisterHome;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel
{
    public class UserWithoutPartOfHomePageViewModel : BaseViewModel
    {
        public ICommand CardTapped { get; }

        public UserWithoutPartOfHomePageViewModel()
        {
            CardTapped = new Command(ClickOnCard);
        }

        private void ClickOnCard()
        {
            try
            {
                Navigation.PushAsync<RequestZipCodeViewModel>((viewModel, page) => viewModel.Model = new Model.RegisterHomeModel());
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
