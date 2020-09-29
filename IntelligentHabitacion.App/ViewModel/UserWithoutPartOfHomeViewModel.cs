using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.ViewModel.RegisterHome;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel
{
    public class UserWithoutPartOfHomeViewModel : BaseViewModel
    {
        private readonly WebSocketAddFriendConnection _webSocketAddFriendConnection;
        private readonly UserPreferences _userPreferences;

        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardJoinHomeTapped { get; }

        public UserWithoutPartOfHomeViewModel(UserPreferences userPreferences)
        {
            _userPreferences = userPreferences;

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
            var navigation = Resolver.Resolve<INavigation>();
            try
            {
                var scanner = new QrCodeService();
                var result = await scanner.Scan();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    await navigation.PushPopupAsync(new LoadingWithMessagesModal(new List<string>{ ResourceText.TITLE_WARMING_UP_ENGINES, ResourceText.TITLE_SENDING_QRCODE, ResourceText.TITLE_VALIDATING_OPERATION, ResourceText.TITLE_WAITING_FOR_ADMINISTRATOR }));
                    var callbackDeclined = new Command(async() =>
                    {
                        await navigation.PushPopupAsync(new OperationErrorModal(ResourceText.TITLE_DECLINED));
                        await Task.Delay(1100);
                        await navigation.PopAllPopupAsync();
                        DisconnectFromSocket();
                    });
                    var callbackApproved = new Command(async () =>
                    {
                        await navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                        _userPreferences.UserIsPartOfOneHome(true);
                        Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                        await Task.Delay(1100);
                        DisconnectFromSocket();
                        await navigation.PopAllPopupAsync();
                    });
                    await _webSocketAddFriendConnection.QrCodeWasRead(callbackDeclined, callbackApproved, _userPreferences.Token, result);
                }
            }
            catch (System.Exception exeption)
            {
                await navigation.PopPopupAsync();
                await Exception(exeption);
            }
        }

        private async Task HandleException(string message)
        {
            DisconnectFromSocket();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopAllPopupAsync();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        public void DisconnectFromSocket()
        {
            Task.Run(async () => await _webSocketAddFriendConnection.StopConnection());
        }
    }
}
