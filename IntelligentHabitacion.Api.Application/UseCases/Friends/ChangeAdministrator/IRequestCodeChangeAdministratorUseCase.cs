using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeAdministrator
{
    public interface IRequestCodeChangeAdministratorUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
