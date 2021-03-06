using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(RequestResetYourPasswordJson resetYourPasswordJson);
    }
}
