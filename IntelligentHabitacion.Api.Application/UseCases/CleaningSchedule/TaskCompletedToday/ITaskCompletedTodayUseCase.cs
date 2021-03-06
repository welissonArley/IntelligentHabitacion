using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.TaskCompletedToday
{
    public interface ITaskCompletedTodayUseCase
    {
        Task<ResponseOutput> Execute(long taskId);
    }
}
