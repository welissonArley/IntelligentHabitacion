﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using Plugin.Clipboard;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel
{
    public class HomeInformationViewModel : BaseViewModel
    {
        private string _currentZipCode;
        private readonly IHomeRule _homeRule;
        
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public System.EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; set; }

        public ICommand UpdateInformationsTapped { get; }
        public ICommand DeleteHomeTapped { get; }
        public ICommand CopyWifiPasswordTapped { get; }

        public HomeInformationViewModel(IHomeRule homeRule, UserPreferences userPreferences)
        {
            IsAdministrator = userPreferences.IsAdministrator;
            _homeRule = homeRule;
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
            DeleteHomeTapped = new Command(async () => await ClickDeleteHome());
            CopyWifiPasswordTapped = new Command(async () => await ClickCopyWifiPassword());
            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };
            Model = Task.Run(async () => await homeRule.GetInformations()).Result;
            _currentZipCode = Model.ZipCode;
        }

        private async Task ClickCopyWifiPassword()
        {
            if (!string.IsNullOrWhiteSpace(Model.NetWork.Password))
            {
                CrossClipboard.Current.SetText(Model.NetWork.Password);
                await ShowQuickInformation(ResourceText.INFORMATION_PASSWORD_COPIED_SUCCESSFULLY);
            }
        }
        private async Task ClickUpdateInformations()
        {
            try
            {
                await ShowLoading();
                await _homeRule.UpdateInformations(Model);
                HideLoading();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickDeleteHome()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<DeleteHomeViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task VerifyZipCode()
        {
            try
            {
                if (_currentZipCode.Equals(Model.ZipCode))
                    return;

                await ShowLoading();
                if (Application.Current.MainPage.Navigation.NavigationStack.Count == 1)
                {
                    HideLoading();
                    return;
                }
                var result = await _homeRule.ValidadeZipCode(Model.ZipCode);
                
                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Street;
                Model.City.Name = result.City;
                Model.City.State.Name = result.State.Name;
                Model.City.State.Country.Name = result.State.Country.Name;

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}
