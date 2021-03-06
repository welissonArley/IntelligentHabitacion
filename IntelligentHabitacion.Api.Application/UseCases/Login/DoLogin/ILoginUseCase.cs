using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Login.DoLogin
{
    public interface ILoginUseCase
    {
        Task<ResponseOutput> Execute(RequestLoginJson loginJson);
    }
}
