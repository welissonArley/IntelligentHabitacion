using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTaskFeedbacks
{
    public interface IGetTaskFeedbacksUseCase
    {
        Task<ResponseOutput> Execute(long taskCompletedId);
    }
}
