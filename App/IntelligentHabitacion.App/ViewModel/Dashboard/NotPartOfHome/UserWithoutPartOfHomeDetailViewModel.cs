using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Dashboard.PartOfHome;
using IntelligentHabitacion.App.View.Home.Register;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.View.User.Update;
using IntelligentHabitacion.App.ViewModel.Home.Register;
using IntelligentHabitacion.App.ViewModel.User.Update;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Dashboard.NotPartOfHome
{
    public class UserWithoutPartOfHomeDetailViewModel
    {
        private WebSocketAddFriendConnection _webSocketAddFriendConnection;
        private readonly INavigation Navigation;

        public ICommand CardCreateHomeTapped { get; }
        public ICommand CardMyInformationTapped { get; }
        public ICommand CardJoinHomeTapped { get; }

        public UserWithoutPartOfHomeDetailViewModel(INavigation navigation)
        {
            Navigation = navigation;

            CardCreateHomeTapped = new Command(async () => await ClickOnCardCreateHome());
            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardJoinHomeTapped = new Command(async () => await ClickOnCardJoinHome());
        }

        private async Task ClickOnCardJoinHome()
        {
            try
            {
                var scanner = new QrCodeService();
                var result = await scanner.Scan();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var userPreferences = Resolver.Resolve<UserPreferences>();

                    await Navigation.PushPopupAsync(new LoadingWithMessagesModal(new List<string> { ResourceText.TITLE_WARMING_UP_ENGINES, ResourceText.TITLE_SENDING_QRCODE, ResourceText.TITLE_VALIDATING_OPERATION, ResourceText.TITLE_WAITING_FOR_ADMINISTRATOR }));
                    var callbackDeclined = new Command(async () =>
                    {
                        await Navigation.PushPopupAsync(new OperationErrorModal(ResourceText.TITLE_DECLINED));
                        await Task.Delay(1100);
                        await Navigation.PopAllPopupAsync();
                        await DisconnectFromSocket();
                    });
                    var callbackApproved = new Command(async () =>
                    {
                        await Navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                        userPreferences.UserIsPartOfOneHome(true);
                        Application.Current.MainPage = new NavigationPage(new UserIsPartOfHomeFlyoutPage());
                        await Task.Delay(1100);
                        await DisconnectFromSocket();
                        await Navigation.PopAllPopupAsync();
                    });
                    CreateConnectionSocket();
                    await _webSocketAddFriendConnection.QrCodeWasRead(callbackDeclined, callbackApproved, await userPreferences.GetToken(), result);
                }
            }
            catch
            {
                await Navigation.PopPopupAsync();
            }
        }
        public async Task DisconnectFromSocket()
        {
            await _webSocketAddFriendConnection.StopConnection().ConfigureAwait(false);
            _webSocketAddFriendConnection = null;
        }
        private void CreateConnectionSocket()
        {
            var callbackWhenAnErrorOccurs = new Command(async (message) =>
            {
                await HandleException(message.ToString());
            });

            _webSocketAddFriendConnection = new WebSocketAddFriendConnection();
            _webSocketAddFriendConnection.SetCallbacks(callbackWhenAnErrorOccurs, null);
        }
        private async Task HandleException(string message)
        {
            await DisconnectFromSocket();
            await Navigation.PopAllPopupAsync();
            await Navigation.PushPopupAsync(new ErrorModal(message));
        }

        private async Task ClickOnCardCreateHome()
        {
            var page = (Page)ViewFactory.CreatePage<SelectCountryViewModel, SelectCountryPage>((viewModel, _) =>
            {
                viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardMyInformations()
        {
            var page = (Page)ViewFactory.CreatePage<UserInformationViewModel, UserInformationPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
    }
}
