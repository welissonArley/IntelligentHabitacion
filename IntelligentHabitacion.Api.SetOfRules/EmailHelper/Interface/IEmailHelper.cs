namespace IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface
{
    public interface IEmailHelper
    {
        void ResetPassword(string email, string code, string userName);
    }
}
