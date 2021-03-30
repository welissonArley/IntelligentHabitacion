using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleWriteOnlyRepository
    {
        Task Add(IEnumerable<Entity.CleaningSchedule> schedules);
        void FinishAllFromTheUser(long userId, long homeId);
    }
}
