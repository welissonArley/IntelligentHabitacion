using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleWriteOnlyRepository
    {
        Task Add(IEnumerable<Entity.CleaningSchedule> schedules);
        void FinishSchedules(IList<long> scheduleIds);
        Task CompletedTask(long taskScheduleId);
        Task<int> AddRateTask_ReturnAverageRating(Entity.CleaningRating rate, long userIdWhoRateTheTask);
    }
}
