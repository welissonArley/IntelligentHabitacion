using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Calendar
{
    public interface ICalendarUseCase
    {
        Task<ResponseOutput> Execute(RequestCalendarCleaningScheduleJson request);
    }
}
