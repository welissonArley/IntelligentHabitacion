using IntelligentHabitacion.App.SQLite.Interface;
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
        protected async Task Exception(System.Exception exception)
        {
            var navigation = Resolver.Resolve<INavigation>();

            if (!(exception.InnerException as System.Reflection.TargetInvocationException is null))
            {
                var targetInvocationException = (System.Reflection.TargetInvocationException)exception.InnerException;
                if (!(targetInvocationException.InnerException.InnerException as TokenExpiredException is null))
                    await SecurityTokenExpired(navigation);
                else
                    UnknownError();
            }
            else if (!((exception as TokenExpiredException) is null))
                await SecurityTokenExpired(navigation);
            else if (!((exception as ResponseException) is null))
            {
                var responseException = (ResponseException)exception;
                var database = Resolver.Resolve<ISqliteDatabase>();

                if(!string.IsNullOrWhiteSpace(responseException.Token))
                    database.UpdateToken(responseException.Token);

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
            else if (!((exception as IntelligentHabitacionException) is null))
                await navigation.PushPopupAsync(new ErrorModal(((IntelligentHabitacionException)exception).Message));
            else if (!CrossConnectivity.Current.IsConnected)
                await ErrorInternetConnection();
            else
                UnknownError();
        }

        protected async Task ShowLoading()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new LoadingContentView());
        }

        protected void HideLoading()
        {
            var navigation = Resolver.Resolve<INavigation>();
            navigation.PopPopupAsync();
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
            var database = Resolver.Resolve<ISqliteDatabase>();
            database.Delete();
            await navigation.PopAllPopupAsync();
            await navigation.PushPopupAsync(new ErrorModal(ResourceText.TITLE_PLEASE_LOGIN_AGAIN));
            Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>());
        }
    }
}
