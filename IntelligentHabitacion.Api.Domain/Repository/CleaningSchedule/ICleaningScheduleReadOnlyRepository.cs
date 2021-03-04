using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleReadOnlyRepository
    {
        Task<IList<Dto.MyTasksCleaningScheduleDto>> GetTasksUser(long userId, long homeId, DateTime date);
        Task<IList<Entity.CleaningSchedule>> GetCurrentScheduleForHome(long homeId);
        Task<bool> HomeHasCleaningScheduleCreated(long homeId);
        Task<IList<Entity.CleaningSchedule>> GetCurrentUserSchedules(long userId, long homeId);
    }
}
