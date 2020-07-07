namespace IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface
{
    public interface IEmailHelper
    {
        void ResetPassword(string email, string code, string userName);
        void ChangeAdmin(string adminEmail, string code, string adminName);
        void RemoveFriend(string adminEmail, string code, string adminName);
        void DeleteHome(string adminEmail, string code, string adminName);
    }
}
