using IntelligentHabitacion.Exception.ExceptionsBase;
using Xamarin.Forms;

namespace IntelligentHabitacion.View
{
    public class BasePage : ContentPage
    {
        public void Exception(System.Exception exception)
        {
            if (!((exception as IntelligentHabitacionException) is null))
                DisplayAlert("Erro", exception.Message, "Ok");
            else
                UnknownError();
        }

        private void UnknownError()
        {
            DisplayAlert("Erro", "Erro desconhecido", "Ok");
        }
    }
}
