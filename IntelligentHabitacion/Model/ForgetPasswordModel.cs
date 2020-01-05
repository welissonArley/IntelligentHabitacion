using XLabs.Data;

namespace IntelligentHabitacion.Model
{
    public class ForgetPasswordModel : ObservableObject
    {
        public string Email { get; set; }
        public string CodeReceived { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
