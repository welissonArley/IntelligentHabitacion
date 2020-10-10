using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.Home.Delete
{
    public class DeleteHomeViewModel : BaseViewModel
    {
        private readonly UserPreferences _userPreferences;
        private readonly IHomeRule _homeRule;

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public DeleteHomeViewModel(IHomeRule homeRule, UserPreferences userPreferences)
        {
            _userPreferences = userPreferences;
            _homeRule = homeRule;

            CancelCommand = new Command(async () => await OnCancel());
            ConfirmCommand = new Command(async () => await OnConfirm());
        }

        private async Task OnCancel()
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task OnConfirm()
        {
            try
            {
                await ShowLoading();
                await _homeRule.RequestCodeDeleteHome();
                await Navigation.PushAsync<ApproveActionWithCodePasswordViewModel>((viewModel, page) =>
                {
                    viewModel.Action = Action.DeleteHome;
                    viewModel.FunctionCallbackCommand = new Command(async () =>
                    {
                        await ShowLoading();
                        await Navigation.PopToRootAsync();
                        _userPreferences.UserIsAdministrator(false);
                        _userPreferences.UserIsPartOfOneHome(false);
                        Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
                        HideLoading();
                    });
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
