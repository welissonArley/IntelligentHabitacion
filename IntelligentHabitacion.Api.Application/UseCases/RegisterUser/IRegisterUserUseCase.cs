using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterUserJson registerUserJson);
    }
}
