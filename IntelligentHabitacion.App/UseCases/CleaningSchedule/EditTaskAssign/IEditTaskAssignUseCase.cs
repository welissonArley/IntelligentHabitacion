using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.EditTaskAssign
{
    public interface IEditTaskAssignUseCase
    {
        Task Execute(List<string> userIds, string room);
    }
}
