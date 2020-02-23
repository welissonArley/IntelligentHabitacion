using IntelligentHabitacion.App.ViewModel.RegisterHome;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserWithoutPartOfHomePageViewModel : BaseViewModel
    {
        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }

        public UserWithoutPartOfHomePageViewModel()
        {
            CardCreateHomeTapped = new Command(ClickOnCardCreateHome);
            CardMyInformationTapped = new Command(ClickOnCardMyInformations);
        }

        private void ClickOnCardCreateHome()
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
        private void ClickOnCardMyInformations()
        {
            try
            {
                Navigation.PushAsync<UpdateUserInformationViewModel>();
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
