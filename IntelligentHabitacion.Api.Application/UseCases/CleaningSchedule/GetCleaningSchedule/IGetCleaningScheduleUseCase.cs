using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetCleaningSchedule
{
    public interface IGetCleaningScheduleUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
