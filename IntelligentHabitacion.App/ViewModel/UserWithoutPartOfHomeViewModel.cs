using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.ViewModel.RegisterHome;
using IntelligentHabitacion.App.WebSocket;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserWithoutPartOfHomeViewModel : BaseViewModel
    {
        private readonly WebSocketAddFriendConnection _webSocketAddFriendConnection;
        private readonly ISqliteDatabase _sqliteDatabase;

        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardJoinHomeTapped { get; }

        public UserWithoutPartOfHomeViewModel(ISqliteDatabase sqliteDatabase)
        {
            _sqliteDatabase = sqliteDatabase;

            CardCreateHomeTapped = new Command(async () => await ClickOnCardCreateHome());
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardJoinHomeTapped = new Command(async () => await ClickOnCardJoinHome());

            var callbackWhenAnErrorOccurs = new Command(async (message) =>
            {
                await HandleException(message.ToString());
            });

            _webSocketAddFriendConnection = new WebSocketAddFriendConnection();
            _webSocketAddFriendConnection.SetCallbacks(callbackWhenAnErrorOccurs, null);
        }

        private async Task ClickOnCardCreateHome()
        {
            try
            {
                await Navigation.PushAsync<RequestZipCodeViewModel>((viewModel, page) => viewModel.Model = new Model.HomeModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardMyInformations()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<UpdateUserInformationViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickOnCardJoinHome()
        {
            try
            {
                var scanner = new QrCodeService();
                var result = await scanner.Scan();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    await ShowLoading();
                    var callbackDeclined = new Command(async() =>
                    {
                        var navigation = Resolver.Resolve<INavigation>();
                        await navigation.PushPopupAsync(new OperationErrorModal(ResourceText.TITLE_DECLINED));
                        await Task.Delay(1100);
                        await navigation.PopAllPopupAsync();
                        DisconnectFromSocket();
                    });
                    var callbackApproved = new Command(async () =>
                    {
                        var navigation = Resolver.Resolve<INavigation>();
                        await navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                        await Task.Delay(1100);
                        await navigation.PopAllPopupAsync();
                        DisconnectFromSocket();
                    });
                    await _webSocketAddFriendConnection.QrCodeWasRead(callbackDeclined, callbackApproved, _sqliteDatabase.Get().Token, result);
                }
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private async Task HandleException(string message)
        {
            DisconnectFromSocket();
            HideLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        public void DisconnectFromSocket()
        {
            Task.Run(async () => await _webSocketAddFriendConnection.StopConnection());
        }
    }
}
