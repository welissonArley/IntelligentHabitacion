﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.RegisterHome
{
    public class RequestDeadlinePaymentRentViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public HomeModel Model { get; set; }

        public RequestDeadlinePaymentRentViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(async () => await OnNext());
        }

        private async Task OnNext()
        {
            try
            {
                _homeRule.ValidadeDeadlinePaymentRent(Model.DeadlinePaymentRent);
                await Navigation.PushAsync<RequestNetworkInformationViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
