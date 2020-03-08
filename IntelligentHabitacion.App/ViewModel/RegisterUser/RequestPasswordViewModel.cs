using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.RegisterUser
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        public ICommand OnConcludeCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public RequestPasswordViewModel(IUserRule userRule)
        {
            _userRule = userRule;
            OnConcludeCommand = new Command(OnConclude);
        }

        private async void OnConclude()
        {
            try
            {
                ShowLoading();

                _userRule.ValidatePassword(Model.Password, Model.PasswordConfirmation);

                await _userRule.Create(Model);

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomePageViewModel, UserWithoutPartOfHomePage>());

                HideLoading();

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                Exception(exeption);
            }
        }
    }
}
