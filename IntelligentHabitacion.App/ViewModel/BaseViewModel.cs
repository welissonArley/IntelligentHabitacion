using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Template.Loading;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel
{
    public class BaseViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private LoadingContentView _loadingContentView;

        protected async Task Exception(System.Exception exception)
        {
            var navigation = Resolver.Resolve<INavigation>();

            if (!(exception.InnerException as System.Reflection.TargetInvocationException is null))
                await TargetInvocationException((System.Reflection.TargetInvocationException)exception.InnerException, navigation);
            else if (!((exception as TokenExpiredException) is null))
                await SecurityTokenExpired(navigation);
            else if (!((exception as ResponseException) is null))
                await ResponseException((ResponseException)exception, navigation);
            else if (!((exception as IntelligentHabitacionException) is null))
                await navigation.PushPopupAsync(new ErrorModal(((IntelligentHabitacionException)exception).Message));
            else if (!CrossConnectivity.Current.IsConnected)
                await ErrorInternetConnection();
            else
                UnknownError();
        }

        protected async Task ShowLoading()
        {
            _loadingContentView = new LoadingContentView();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(_loadingContentView);
        }

        protected void HideLoading()
        {
            var navigation = Resolver.Resolve<INavigation>();
            navigation.RemovePopupPageAsync(_loadingContentView);
            _loadingContentView = null;
        }

        protected async Task ShowQuickInformation(string message)
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new QuickInformationModal(message));
            await Task.Delay(1100);
            await navigation.PopPopupAsync();
        }

        #region Exceptions

        private async Task TargetInvocationException(System.Reflection.TargetInvocationException targetInvocationException, INavigation navigation)
        {
            if (!(targetInvocationException.InnerException.InnerException as TokenExpiredException is null))
                await SecurityTokenExpired(navigation);
            else if (!CrossConnectivity.Current.IsConnected)
                await ErrorInternetConnection();
            else if (!((targetInvocationException.InnerException.InnerException as ResponseException) is null))
                await ResponseException((ResponseException)targetInvocationException.InnerException.InnerException, navigation);
            else
                UnknownError();
        }
        private async Task ResponseException(ResponseException responseException, INavigation navigation)
        {
            var userPreferences = Resolver.Resolve<UserPreferences>();

            if (!string.IsNullOrWhiteSpace(responseException.Token))
                userPreferences.Token = responseException.Token;

            if (!((responseException.Exception as ErrorOnValidationException) is null))
            {
                ErrorOnValidationException validacaoException = (ErrorOnValidationException)responseException.Exception;
                await navigation.PushPopupAsync(new ErrorModal("- " + string.Join("\n- ", validacaoException.ErrorMensages)));
            }
            else if (!((responseException.Exception as IntelligentHabitacionException) is null))
                await navigation.PushPopupAsync(new ErrorModal(((IntelligentHabitacionException)responseException.Exception).Message));
            else
                UnknownError();
        }
        
        private void UnknownError()
        {
            var navigation = Resolver.Resolve<INavigation>();
            navigation.PushPopupAsync(new ErrorModal(ResourceTextException.UNKNOW_ERROR));
        }
        private async Task ErrorInternetConnection()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new WithoutInternetConnectionModal());
            await Task.Delay(1100);
            await navigation.PopPopupAsync();
        }
        private async Task SecurityTokenExpired(INavigation navigation)
        {
            var userPreferences = Resolver.Resolve<UserPreferences>();
            userPreferences.ClearAll();
            await navigation.PopAllPopupAsync();
            await navigation.PushPopupAsync(new ErrorModal(ResourceText.TITLE_PLEASE_LOGIN_AGAIN));
            Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>());
        }

        #endregion
    }
}
