using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleReadOnlyRepository
    {
        Task<IList<Dto.MyTasksCleaningScheduleDto>> GetTasksUser(long userId, long homeId, DateTime date);
        Task<bool> HomeHasCleaningScheduleCreated(long homeId);
    }
}
