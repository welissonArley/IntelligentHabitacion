using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetMyTasksCleaningSchedule
{
    public interface IGetMyTasksCleaningScheduleUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
