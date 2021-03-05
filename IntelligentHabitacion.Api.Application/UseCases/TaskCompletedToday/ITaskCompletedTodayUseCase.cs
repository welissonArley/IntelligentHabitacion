using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.TaskCompletedToday
{
    public interface ITaskCompletedTodayUseCase
    {
        Task<ResponseOutput> Execute(long taskId);
    }
}
