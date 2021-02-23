using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.ViewModel.Home.Delete;
using Plugin.Clipboard;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.Home.Informations
{
    public abstract class HomeInformationBaseViewModel : BaseViewModel
    {
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public ICommand DeleteHomeTapped { get; }
        public ICommand CopyWifiPasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; protected set; }
        public ICommand AddRoomTapped { get; }
        public ICommand RemoveRoomTapped { get; }

        public HomeInformationBaseViewModel()
        {
            DeleteHomeTapped = new Command(async () => await ClickDeleteHome());
            CopyWifiPasswordTapped = new Command(async () => await ClickCopyWifiPassword());
            AddRoomTapped = new Command(async () => await ClickAddRoom());
            RemoveRoomTapped = new Command((room) => ClickRemoveRoom(room.ToString()));
        }

        protected async Task ClickAddRoom()
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

        protected void ClickRemoveRoom(string room)
        {
            var model = Model.Rooms.First(c => c.Room.Equals(room));
            Model.Rooms.Remove(model);

            Model.Rooms = new System.Collections.ObjectModel.ObservableCollection<RoomModel>(Model.Rooms);

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }

        protected async Task ClickDeleteHome()
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

        protected async Task ClickCopyWifiPassword()
        {
            if (!string.IsNullOrWhiteSpace(Model.NetWork.Password))
            {
                CrossClipboard.Current.SetText(Model.NetWork.Password);
                await ShowQuickInformation(ResourceText.INFORMATION_PASSWORD_COPIED_SUCCESSFULLY);
            }
        }

        protected abstract Task ClickUpdateInformations();
    }
}
