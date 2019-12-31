namespace IntelligentHabitacion.SetOfRules.Interface
{
    public interface ILoginRule
    {
        void ChangePasswordForgetPassword(string code, string newPassword, string confirmationPassword);
        void RequestCode(string email);
    }
}
