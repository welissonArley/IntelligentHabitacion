using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RateTask
{
    public interface IRateTaskUseCase
    {
        Task<ResponseOutput> Execute(long taskCompletedId, RequestRateTaskJson request);
    }
}
