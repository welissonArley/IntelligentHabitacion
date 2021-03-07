using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleReadOnlyRepository
    {
        Task<IList<Entity.CleaningSchedule>> GetAllTasksUser(long userId, long homeId, DateTime date);
        Task<IList<Dto.MyTasksCleaningScheduleDto>> GetMyTasksSimplifiedUser(long userId, long homeId, DateTime date);
        Task<IList<Entity.CleaningSchedule>> GetCurrentScheduleForHome(long homeId);
        Task<bool> HomeHasCleaningScheduleCreated(long homeId);
        Task<IList<Entity.CleaningSchedule>> GetCurrentUserSchedules(long userId, long homeId);
        Task<Entity.CleaningSchedule> GetTaskById(long taskId, long userId, long homeId, bool isFinished = false);
        Task<bool> UserAlreadyRatedTheTask(long userId, long taskCompletedId);
        Task<Entity.CleaningSchedule> GetTaskByCompletedId(long completedId);
        Task<List<Entity.CleaningRating>> GetRates(long completedId);
    }
}
