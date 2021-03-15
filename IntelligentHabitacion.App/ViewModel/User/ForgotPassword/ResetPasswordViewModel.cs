using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.UseCases.Login.ForgotPassword;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.User.ForgotPassword
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly Lazy<IResetPasswordUseCase> useCase;
        private IResetPasswordUseCase _useCase => useCase.Value;

        public ICommand ChangePasswordCommand { protected set; get; }

        public ForgetPasswordModel Model { get; set; }

        public ResetPasswordViewModel(Lazy<IResetPasswordUseCase> useCase)
        {
            this.useCase = useCase;
            ChangePasswordCommand = new Command(async () => await OnChangePassword());
        }

        private async Task OnChangePassword()
        {
            try
            {
                SendingData();

                await _useCase.Execute(Model);

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
