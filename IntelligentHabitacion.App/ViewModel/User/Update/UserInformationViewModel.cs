using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.UseCases.User.UpdateUserInformations;
using IntelligentHabitacion.App.UseCases.User.UserInformations;
using IntelligentHabitacion.App.ViewModel.User.Delete;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.User.Update
{
    public class UserInformationViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private Lazy<IUserInformationsUseCase> getInformationsUseCase;
        private IUserInformationsUseCase _useCase => getInformationsUseCase.Value;
        private Lazy<IUpdateUserInformationsUseCase> updateUseCase;
        private IUpdateUserInformationsUseCase _updateUseCase => updateUseCase.Value;

        public ICommand DeleteAccountTapped { get; }
        public ICommand ChangePasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; }

        public UserInformationsModel Model { get; set; }

        public UserInformationViewModel(Lazy<IUserInformationsUseCase> getInformationsUseCase,
            Lazy<IUpdateUserInformationsUseCase> updateUseCase,
            Lazy<UserPreferences> userPreferences)
        {
            CurrentState = LayoutState.Loading;

            this.userPreferences = userPreferences;
            this.getInformationsUseCase = getInformationsUseCase;
            this.updateUseCase = updateUseCase;

            DeleteAccountTapped = new Command(async () => await ClickDeleteAccount());
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
        }

        private async Task ClickDeleteAccount()
        {
            try
            {
                await Navigation.PushAsync<ConfirmDeleteAccountViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                await Navigation.PushAsync<ChangePasswordViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task ClickUpdateInformations()
        {
            try
            {
                Saving();

                await _updateUseCase.Execute(Model);

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            Model = await _useCase.Execute();
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
