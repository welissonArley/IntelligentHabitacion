using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.UseCases.Home.HomeInformations;
using IntelligentHabitacion.App.UseCases.Home.RegisterHome.Brazil;
using Plugin.Clipboard;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Informations
{
    public class HomeInformationViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private readonly Lazy<IRequestCEPUseCase> cepUseCase;
        private readonly Lazy<IHomeInformationsUseCase> informationsUseCase;
        private IHomeInformationsUseCase _informationsUseCase => informationsUseCase.Value;
        private IRequestCEPUseCase _cepUseCase => cepUseCase.Value;
        private UserPreferences _userPreferences => userPreferences.Value;

        public string _currentZipCode;
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public ICommand CopyWifiPasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; }
        public ICommand AddRoomTapped { get; }
        public ICommand RemoveRoomTapped { get; }

        public EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; set; }

        public HomeInformationViewModel(Lazy<UserPreferences> userPreferences, Lazy<IRequestCEPUseCase> cepUseCase,
            Lazy<IHomeInformationsUseCase> informationsUseCase)
        {
            this.userPreferences = userPreferences;
            this.cepUseCase = cepUseCase;
            this.informationsUseCase = informationsUseCase;

            CurrentState = LayoutState.Loading;

            CopyWifiPasswordTapped = new Command(async () => await ClickCopyWifiPassword());
            AddRoomTapped = new Command(async () => await ClickAddRoom());
            RemoveRoomTapped = new Command((room) => ClickRemoveRoom(room.ToString()));
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };
        }

        private async Task ClickCopyWifiPassword()
        {
            if (!string.IsNullOrWhiteSpace(Model.NetWork.Password))
            {
                CrossClipboard.Current.SetText(Model.NetWork.Password);
                await ShowQuickInformation(ResourceText.INFORMATION_PASSWORD_COPIED_SUCCESSFULLY);
            }
        }

        private async Task ClickAddRoom()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<InsertRoomViewModel>((viewModel, page) =>
                {
                    viewModel.RoomsSaved = Model.Rooms.Select(c => c.Room).ToList();
                    viewModel.CallbackSelectRoomCommand = new Command((room) =>
                    {
                        Model.Rooms = new System.Collections.ObjectModel.ObservableCollection<RoomModel>(Model.Rooms)
                        {
                            new RoomModel
                            {
                                Room = room.ToString()
                            }
                        };

                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    });
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private void ClickRemoveRoom(string room)
        {
            var model = Model.Rooms.First(c => c.Room.Equals(room));
            Model.Rooms.Remove(model);

            Model.Rooms = new ObservableCollection<RoomModel>(Model.Rooms);

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }

        private async Task ClickUpdateInformations()
        {
            try
            {
                //await _homeRule.UpdateInformations(Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task VerifyZipCode()
        {
            try
            {
                if (Model.City.Country.Id != Useful.CountryEnum.BRAZIL || _currentZipCode.Equals(Model.ZipCode))
                    return;

                await ShowLoading();

                var result = await _cepUseCase.Execute(Model.ZipCode);

                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Address;
                Model.City.Name = result.City.Name;
                Model.City.StateProvinceName = result.City.StateProvinceName;

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            Model = await _informationsUseCase.Execute();
            IsAdministrator = _userPreferences.IsAdministrator;
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("IsAdministrator"));
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
