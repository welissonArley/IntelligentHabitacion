using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.DetailsAllRate
{
    public interface IDetailsAllRateUseCase
    {
        Task<ResponseOutput> Execute(long completedTaskId);
    }
}
