using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task<ResponseOutput> Execute(RequestChangePasswordJson changePasswordJson);
    }
}
