using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.User.RegisterUser;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel.Login;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.ViewModel.User.Register
{
    public class RequestPasswordViewModel : BaseViewModel
    {
        private readonly Lazy<IRegisterUserUseCase> useCase;
        private IRegisterUserUseCase _useCase => useCase.Value;

        public ICommand OnConcludeCommand { get; }
        public ICommand ShowHidePasswordCommand { get; }

        public RegisterUserModel Model { get; set; }

        public bool IsPassword { get; set; }
        public string IlustrationShowHidePassword { get; set; }
        public int IlustrationHeightRequest { get; set; }
        public Thickness IlustrationMargin { get; set; }

        public RequestPasswordViewModel(Lazy<IRegisterUserUseCase> useCase)
        {
            this.useCase = useCase;
            HidePassword();
            OnConcludeCommand = new Command(async () => await OnConclude());
            ShowHidePasswordCommand = new Command(ShowHidePassword);
        }

        private async Task OnConclude()
        {
            try
            {
                await ShowLoading();

                ValidatePassword(Model.Password);

                await _useCase.Execute(Model);

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

        public void ValidatePassword(string password)
        {
            new PasswordValidator().IsValid(password);
        }
    }
}
