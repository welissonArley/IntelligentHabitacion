using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeAdministrator
{
    public interface IRequestCodeChangeAdministratorUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
