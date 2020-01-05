using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.View.Modal;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.ViewModel
{
    public class BaseViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        public void Exception(System.Exception exception)
        {
            var navigation = Resolver.Resolve<INavigation>();

            if (!((exception as IntelligentHabitacionException) is null))
                navigation.PushPopupAsync(new ErrorModal(exception.Message));
            else
                UnknownError();
        }

        private void UnknownError()
        {
            var navigation = Resolver.Resolve<INavigation>();
            navigation.PushPopupAsync(new ErrorModal(ResourceTextException.UNKNOW_ERROR));
        }
    }
}
