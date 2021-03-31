using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleReadOnlyRepository
    {
        Task<bool> HomeHasCleaningScheduleCreated(long homeId);
        Task<List<Entity.CleaningSchedule>> GetTasksWithMoreThan8daysWithoutClompleted();
        Task<List<Entity.CleaningSchedule>> GetTasksForTheMonth(DateTime month, long homeId);
        Task<bool> TaskCleanedOnDate(long taskId, DateTime date);
        Task<bool> ThereAreaTaskToUserRateThisMonth(long userId, string room);
        Task<Entity.CleaningSchedule> GetTaskById(long id);
    }
}
