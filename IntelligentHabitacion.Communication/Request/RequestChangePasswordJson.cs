namespace IntelligentHabitacion.Communication.Request
{
    public class RequestChangePasswordJson
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
