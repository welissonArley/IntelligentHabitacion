using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.EditTaskAssign
{
    public interface IEditTaskAssignUseCase
    {
        Task<ResponseOutput> Execute(RequestEditAssignCleaningScheduleJson request);
    }
}
