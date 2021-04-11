using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleWriteOnlyRepository
    {
        Task Add(IEnumerable<Entity.CleaningSchedule> schedules);
        Task FinishAllFromTheUser(long userId, long homeId);
        Task RegisterRoomCleaned(long taskId, DateTime date);
        void Remove(Entity.CleaningSchedule schedule);
        Task FinishTask(long taskId);
        Task<int> AddRateTask_ReturnAverageRating(Entity.CleaningRating cleaningRating, long userThatGiveTheRate);
    }
}
