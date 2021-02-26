using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.App.ViewModel.User.Update;
using IntelligentHabitacion.Useful;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Login
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
                await ShowLoading();
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new ChooseCountryModal(new Command(async (value) =>
                {
                    await OnCountrySelectedAsync((CountryModel)value);
                })));
                HideLoading();
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
                await Navigation.PushAsync<UserInformationViewModel>();
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
                        await DisconnectFromSocket();
                    });
                    var callbackApproved = new Command(async () =>
                    {
                        await navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                        _userPreferences.UserIsPartOfOneHome(true);
                        Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserIsPartOfHomeViewModel, UserIsPartOfHomePage>());
                        await Task.Delay(1100);
                        await DisconnectFromSocket();
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
            await DisconnectFromSocket();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopAllPopupAsync();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }

        public async Task DisconnectFromSocket()
        {
            await _webSocketAddFriendConnection.StopConnection().ConfigureAwait(false);
        }

        private async Task OnCountrySelectedAsync(CountryModel value)
        {
            if (value.Id == CountryEnum.BRAZIL)
                await Navigation.PushAsync<Home.Register.Brazil.RequestZipCodeViewModel>((viewModel, page) => viewModel.Country = value);
            else
            {
                await Navigation.PushAsync<Home.Register.Others.RegisterHomeViewModel>((viewModel, page) => viewModel.Model = new Model.HomeModel
                {
                    City = new Model.CityModel
                    {
                        Country = value
                    }
                });
            }
        }
    }
}
