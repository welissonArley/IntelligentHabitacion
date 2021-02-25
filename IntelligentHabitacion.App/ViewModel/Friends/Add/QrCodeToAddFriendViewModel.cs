using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.Communication.Response;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.Add
{
    public class QrCodeToAddFriendViewModel : BaseViewModel
    {
        private readonly WebSocketAddFriendConnection _webSocketAddFriendConnection;
        public string Time { get; set; }
        public bool ReceivedCode { get; set; }
        public string QrCode { get; set; }
        public string ProfileColor { get; set; }

        public ICommand CancelOperationTapped { get; set; }
        public ICommand ApprovedOperation { get; set; }

        public QrCodeToAddFriendViewModel(UserPreferences userPreferences)
        {
            ProfileColor = userPreferences.ProfileColor;

            CancelOperationTapped = new Command(async () =>
            {
                await OnCancelOperation();
            });

            var callbackWhenAnErrorOccurs = new Command(async (message) =>
            {
                await HandleException(message.ToString());
            });
            var callbackTimeChanged = new Command(async (time) =>
            {
                await OnChangedTime((int)time);
            });
            var callbackCodeIsReceived = new Command((code) =>
            {
                OnCodeReceived(code.ToString());
            });
            var callbackCodeWasRead = new Command(async(newFriendToAddJson) =>
            {
                await OnCodeWasRead((ResponseFriendJson)newFriendToAddJson);
            });

            _webSocketAddFriendConnection = new WebSocketAddFriendConnection();
            _webSocketAddFriendConnection.SetCallbacks(callbackWhenAnErrorOccurs, callbackTimeChanged);
            
            Device.BeginInvokeOnMainThread(async () => await _webSocketAddFriendConnection.GetQrCodeToAddFriend(callbackCodeIsReceived, callbackCodeWasRead, userPreferences.Token));
        }

        private void OnCodeReceived(string code)
        {
            ReceivedCode = true;
            QrCode = code;
            OnPropertyChanged(new PropertyChangedEventArgs("QrCode"));
            OnPropertyChanged(new PropertyChangedEventArgs("ReceivedCode"));
        }

        private async Task OnChangedTime(int timer)
        {
            if (timer > 0)
            {
                Time = DateTime.Today.AddSeconds(timer).ToString("mm:ss");
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
            else
            {
                DisconnectFromSocket();
                await HandleException(ResourceText.TITLE_TIME_EXPIRED_TRY_AGAIN);
            }
        }

        private async Task OnCancelOperation()
        {
            await Navigation.PopToRootAsync();
            DisconnectFromSocket();
        }

        private async Task OnCodeWasRead(ResponseFriendJson newFriendToAddJson)
        {
            try
            {
                await Navigation.PushAsync<AcceptNewFriendViewModel>((viewModel, page) =>
                {
                    viewModel.WebSocketAddFriendConnection = _webSocketAddFriendConnection;
                    viewModel.Model = new Model.AcceptNewFriendModel
                    {
                        Name = newFriendToAddJson.Name,
                        ProfileColor = newFriendToAddJson.ProfileColor,
                        EntryDate = DateTime.Today,
                        MonthlyRent = 0
                    };
                    viewModel.NewFriendToAddJson = newFriendToAddJson;
                    viewModel.ApprovedOperation = ApprovedOperation;
                });
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task HandleException(string message)
        {
            await Navigation.PopAsync();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        public void DisconnectFromSocket()
        {
            _webSocketAddFriendConnection.StopConnection().ConfigureAwait(false).GetAwaiter();
        }
    }
}
