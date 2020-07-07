using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel
{
    public class DeleteHomeViewModel : BaseViewModel
    {
        private readonly ISqliteDatabase _database;
        private readonly IHomeRule _homeRule;

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public DeleteHomeViewModel(IHomeRule homeRule, ISqliteDatabase database)
        {
            _database = database;
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
                        _database.IsNotAdministrator();
                        _database.IsNotPartOfHome();
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
