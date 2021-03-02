using IntelligentHabitacion.Api.Domain.Dto;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class CleaningScheduleRepository : ICleaningScheduleReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public CleaningScheduleRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task<IList<MyTasksCleaningScheduleDto>> GetMyTasksCleaningSchedule(long userId, long homeId, DateTime date)
        {
            var response = _context.CleaningSchedules.Where(c => c.Active && c.UserId == userId
                && c.HomeId == homeId && !c.ScheduleFinishAt.HasValue);
            
            return await response.Select(c => new MyTasksCleaningScheduleDto
            {
                Id = c.Id, Room = c.Room,
                CleaningRecords = _context.CleaningTasksCompleteds.Where(w => w.CleaningScheduleId == c.Id).Count(),
                LastRecord = _context.CleaningTasksCompleteds.Where(w => w.CleaningScheduleId == c.Id).OrderBy(c => c.CreateDate).LastOrDefault().CreateDate
            }).ToListAsync();
        }

        public async Task<bool> HomeHasCleaningScheduleCreated(long homeId)
        {
            return await _context.CleaningSchedules.AnyAsync(c => c.HomeId == homeId);
        }
    }
}
