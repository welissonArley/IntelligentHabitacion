﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.ForgotPassword
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;
        public ICommand ChangePasswordCommand { protected set; get; }

        public ForgetPasswordModel Model { get; set; }

        public ChangePasswordViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            ChangePasswordCommand = new Command(async () => await OnChangePassword());
        }

        private async Task OnChangePassword()
        {
            try
            {
                await ShowLoading();
                await _loginRule.ChangePasswordForgotPassword(Model.Email, Model.CodeReceived, Model.NewPassword, Model.PasswordConfirmation);
                HideLoading();
                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
