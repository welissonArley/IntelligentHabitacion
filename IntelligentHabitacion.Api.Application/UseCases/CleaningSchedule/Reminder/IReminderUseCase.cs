using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Reminder
{
    public interface IReminderUseCase
    {
        Task<ResponseOutput> Execute(IList<string> usersId);
    }
}
