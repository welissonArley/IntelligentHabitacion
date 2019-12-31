using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.View.Modal;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace IntelligentHabitacion.View
{
    public class BasePage : ContentPage
    {
        public void Exception(System.Exception exception)
        {
            if (!((exception as IntelligentHabitacionException) is null))
                Navigation.PushPopupAsync(new ErrorModal(exception.Message));
            else
                UnknownError();
        }

        private void UnknownError()
        {
            Navigation.PushPopupAsync(new ErrorModal(ResourceTextException.UNKNOW_ERROR));
        }
    }
}
