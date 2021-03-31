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
    }
}
