using System.Windows.Input;

namespace IntelligentHabitacion.App.ViewModel.ContactUs
{
    public class ContactUsViewModel : BaseViewModel
    {
        public string Message { get; set; }

        public ICommand SendMessageCommand { get; }

        public ContactUsViewModel()
        {

        }
    }
}
