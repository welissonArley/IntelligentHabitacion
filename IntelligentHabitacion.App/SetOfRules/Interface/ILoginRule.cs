using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface ILoginRule
    {
        Task ChangePasswordForgotPassword(string email, string code, string newPassword, string confirmationPassword);
        Task RequestCode(string email);
        Task<ResponseJson> Login(string email, string password);
    }
}
