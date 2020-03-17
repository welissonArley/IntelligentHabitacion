﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.ForgotPassword
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly ILoginRule _loginRule;
        public ICommand RequestCodeCommand { protected set; get; }

        public ForgetPasswordModel Model { get; set; }

        public RequestEmailViewModel(ILoginRule loginRule)
        {
            _loginRule = loginRule;
            RequestCodeCommand = new Command(async () => await OnRequestCode());
        }

        private async Task OnRequestCode()
        {
            try
            {
                _loginRule.RequestCode(Model.Email);

                await Navigation.PushAsync<ChangePasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}