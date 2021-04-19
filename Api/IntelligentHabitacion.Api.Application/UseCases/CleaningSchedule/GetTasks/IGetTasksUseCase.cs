using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTasks
{
    public interface IGetTasksUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
