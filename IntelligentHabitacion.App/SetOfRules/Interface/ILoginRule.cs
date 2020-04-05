using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface ILoginRule
    {
        Task ChangePasswordForgotPassword(ForgetPasswordModel model);
        Task RequestCode(string email);
        Task<ResponseJson> Login(string email, string password);
    }
}
