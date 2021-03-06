using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetMyTasksCleaningSchedule
{
    public interface IGetMyTasksCleaningScheduleUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
