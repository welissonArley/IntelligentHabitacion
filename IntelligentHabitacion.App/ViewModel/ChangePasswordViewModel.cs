﻿using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand ChangePasswordTapped { get; }

        private readonly ILoginRule _loginRule;

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirmation { get; set; }

        public ChangePasswordViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                _loginRule.ChangePassword(CurrentPassword, NewPassword, PasswordConfirmation);
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
