using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel.Login;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.User.Register
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly IUserRule _userRule;
        private readonly UserPreferences _userPreferences;

        public ICommand OnConcludeCommand { protected set; get; }
        public ICommand ShowHidePasswordCommand { protected set; get; }

        public RegisterUserModel Model { get; set; }

        public bool IsPassword { get; set; }
        public string IlustrationShowHidePassword { get; set; }
        public int IlustrationHeightRequest { get; set; }
        public Thickness IlustrationMargin { get; set; }

        public RequestPasswordViewModel(IUserRule userRule, UserPreferences userPreferences)
        {
            _userRule = userRule;
            _userPreferences = userPreferences;
            HidePassword();
            OnConcludeCommand = new Command(async () => await OnConclude());
            ShowHidePasswordCommand = new Command(ShowHidePassword);
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                _userRule.ValidatePassword(Model.Password, Model.PasswordConfirmation);

                var response = await _userRule.Create(Model);

                _userPreferences.SaveUserInformations(new Dtos.UserPreferenceDto
                {
                    IsAdministrator = false,
                    IsPartOfOneHome = false,
                    ProfileColor = response.Response.ToString(),
                    Name = Model.Name,
                    Password = Model.Password,
                    Token = response.Token,
                    Email = Model.Email,
                    Width = Application.Current.MainPage.Width
                });

                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());

                HideLoading();

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private void ShowHidePassword()
        {
            if (IsPassword)
                ShowPassword();
            else
                HidePassword();
        }

        private void ShowPassword()
        {
            IsPassword = false;
            IlustrationShowHidePassword = "IconEye.png";
            IlustrationHeightRequest = 14;
            IlustrationMargin = new Thickness(0, 15, 0, 0);
            OnPropertyChanged();
        }
        private void HidePassword()
        {
            IsPassword = true;
            IlustrationShowHidePassword = "IconEyeHidden.png";
            IlustrationHeightRequest = 18;
            IlustrationMargin = new Thickness(0, 13, 0, 0);
            OnPropertyChanged();
        }

        private void OnPropertyChanged()
        {
            OnPropertyChanged(new PropertyChangedEventArgs("IsPassword"));
            OnPropertyChanged(new PropertyChangedEventArgs("IlustrationShowHidePassword"));
            OnPropertyChanged(new PropertyChangedEventArgs("IlustrationHeightRequest"));
            OnPropertyChanged(new PropertyChangedEventArgs("IlustrationMargin"));
        }
    }
}
