using IntelligentHabitacion.App.Model;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.GetTasks
{
    public interface IGetTasksUseCase
    {
        Task<TasksModel> Execute(DateTime date);
    }
}
