using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class ForgetPasswordModel : ObservableObject
    {
        public string Email { get; set; }
        public string CodeReceived { get; set; }
        public string NewPassword { get; set; }
    }
}
