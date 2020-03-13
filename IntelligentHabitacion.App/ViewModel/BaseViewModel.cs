using IntelligentHabitacion.App.Template.Loading;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel
{
    public class BaseViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        protected async Task Exception(System.Exception exception)
        {
            var navigation = Resolver.Resolve<INavigation>();

            if (!((exception as ErrorOnValidationException) is null))
            {
                ErrorOnValidationException validacaoException = (ErrorOnValidationException)exception;
                await navigation.PushPopupAsync(new ErrorModal("- " + string.Join("\n- ", validacaoException.ErrorMensages)));
            }
            else if (!((exception as IntelligentHabitacionException) is null))
                await navigation.PushPopupAsync(new ErrorModal(exception.Message));
            else if (!CrossConnectivity.Current.IsConnected)
                await ErrorInternetConnection();
            else
                UnknownError();
        }

        protected void ShowLoading()
        {
            var navigation = Resolver.Resolve<INavigation>();
            navigation.PushPopupAsync(new LoadingContentView());
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
    }
}
