using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.CleaningSchedule.Reminder
{
    public interface IReminderUseCase
    {
        Task Execute(IList<string> usersIds);
    }
}
