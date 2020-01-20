﻿using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Model;
using IntelligentHabitacion.SetOfRules.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.ViewModel.RegisterHome
{
    public class RequestZipCodeViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public ICommand NextCommand { protected set; get; }

        public RegisterHomeModel Model { get; set; }

        public RequestZipCodeViewModel(IHomeRule homeRule)
        {
            _homeRule = homeRule;
            NextCommand = new Command(OnNext);
        }

        private async void OnNext()
        {
            try
            {
                var result = await _homeRule.ValidadeZipCode(Model.ZipCode);
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Street;
                Model.City.Name = result.City;
                Model.City.State.Name = result.State.Name;
                Model.City.State.Abbreviation = result.State.Abbreviation;
                Model.City.State.Country.Name = result.State.Country.Name;
                Model.City.State.Country.Abbreviation = result.State.Country.Abbreviation;

                await Navigation.PushAsync<RequestCityViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (RequestException)
            {
                Exception(new ZipCodeInvalidException());
            }
            catch (System.Exception exeption)
            {
                Exception(exeption);
            }
        }
    }
}
