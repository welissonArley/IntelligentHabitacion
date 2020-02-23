namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface ILoginRule
    {
        void ChangePasswordForgetPassword(string email, string code, string newPassword, string confirmationPassword);
        void RequestCode(string email);
        void Login(string email, string password);
        void ChangePassword(string currentPassword, string newPassword, string confirmationPassword);
    }
}
