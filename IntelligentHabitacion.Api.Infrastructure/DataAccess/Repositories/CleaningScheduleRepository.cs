using IntelligentHabitacion.Api.Domain.Dto;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class CleaningScheduleRepository : ICleaningScheduleReadOnlyRepository, ICleaningScheduleWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public CleaningScheduleRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task Add(IEnumerable<CleaningSchedule> schedules)
        {
            await _context.CleaningSchedules.AddRangeAsync(schedules);
        }

        public async Task CompletedTask(long taskScheduleId)
        {
            await _context.CleaningTasksCompleteds.AddAsync(new CleaningTasksCompleted
            {
                CleaningScheduleId = taskScheduleId
            });
        }

        public void FinishSchedules(IList<long> scheduleIds)
        {
            var schedules = _context.CleaningSchedules.Where(c => scheduleIds.Contains(c.Id));
            foreach(var schedule in schedules)
                schedule.ScheduleFinishAt = DateTime.UtcNow;

            if (schedules.Any())
                _context.CleaningSchedules.UpdateRange(schedules);
        }

        public async Task<IList<CleaningSchedule>> GetCurrentScheduleForHome(long homeId)
        {
            return await _context.CleaningSchedules.AsNoTracking().Include(c => c.User)
                .Where(c => c.Active && c.HomeId == homeId && !c.ScheduleFinishAt.HasValue).ToListAsync();
        }

        public async Task<IList<CleaningSchedule>> GetCurrentUserSchedules(long userId, long homeId)
        {
            return await _context.CleaningSchedules.AsNoTracking().Where(c => c.Active && c.UserId == userId
                && c.HomeId == homeId && !c.ScheduleFinishAt.HasValue).ToListAsync();
        }

        public async Task<CleaningSchedule> GetTaskById(long taskId, long userId, long homeId, bool isFinished = false)
        {
            return await _context.CleaningSchedules.AsNoTracking().FirstOrDefaultAsync(c => c.Active && c.Id == taskId
                && c.UserId == userId && c.HomeId == homeId && c.ScheduleFinishAt.HasValue == isFinished);
        }

        public async Task<IList<MyTasksCleaningScheduleDto>> GetMyTasksSimplifiedUser(long userId, long homeId, DateTime date)
        {
            var response = _context.CleaningSchedules.AsNoTracking().Where(c => c.Active && c.UserId == userId
                && c.HomeId == homeId && !c.ScheduleFinishAt.HasValue && c.ScheduleStartAt.Year == date.Year && c.ScheduleStartAt.Month == date.Month);
            
            var dto = await response.Select(c => new MyTasksCleaningScheduleDto
            {
                Id = c.Id, Room = c.Room,
                CleaningRecords = _context.CleaningTasksCompleteds.Where(w => w.CleaningScheduleId == c.Id).Count(),
                LastRecord = _context.CleaningTasksCompleteds.Where(w => w.CleaningScheduleId == c.Id).OrderBy(c => c.CreateDate).LastOrDefault().CreateDate
            }).ToListAsync();

            return dto.OrderByDescending(c => c.CleaningRecords).ThenBy(c => c.Room).ToList();
        }

        public async Task<bool> HomeHasCleaningScheduleCreated(long homeId)
        {
            return await _context.CleaningSchedules.AnyAsync(c => c.HomeId == homeId);
        }

        public async Task<IList<CleaningSchedule>> GetAllTasksUser(long userId, long homeId, DateTime date)
        {
            return await _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.CleaningTasksCompleteds).ThenInclude(c => c.Ratings)
                .Where(c => c.Active && c.UserId == userId
                && c.HomeId == homeId && c.ScheduleStartAt.Year == date.Year && c.ScheduleStartAt.Month == date.Month)
                .OrderBy(c => c.Room)
                .ToListAsync();
        }

        public async Task<bool> UserAlreadyRatedTheTask(long userId, long taskCompletedId)
        {
            return await _context.CleaningRatingUsers.AnyAsync(c => c.UserId == userId && c.CleaningTaskCompletedId == taskCompletedId);
        }

        public async Task<CleaningSchedule> GetTaskByCompletedId(long completedId)
        {
            return await _context.CleaningSchedules.AsNoTracking().Include(c => c.CleaningTasksCompleteds).Include(c => c.User).ThenInclude(c => c.HomeAssociation)
                .FirstOrDefaultAsync(c => c.Active && c.CleaningTasksCompleteds.Any(k => k.Id == completedId));
        }

        public async Task<int> AddRateTask_ReturnAverageRating(CleaningRating rate, long userIdWhoRateTheTask)
        {
            await _context.CleaningRatings.AddAsync(rate);

            await _context.CleaningRatingUsers.AddAsync(new CleaningRatingUser
            {
                CleaningTaskCompletedId = rate.CleaningTaskCompletedId,
                UserId = userIdWhoRateTheTask
            });

            return (await _context.CleaningTasksCompleteds.FirstAsync(c => c.Id == rate.CleaningTaskCompletedId)).AverageRating;
        }

        public async Task<List<CleaningRating>> GetRates(long completedId)
        {
            return await _context.CleaningRatings.AsNoTracking().Where(c => c.CleaningTaskCompletedId == completedId).ToListAsync();
        }

        public async Task<IList<CleaningSchedule>> GetAllTasks(long homeId, DateTime date)
        {
            return await _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.CleaningTasksCompleteds).ThenInclude(c => c.Ratings)
                .Include(c => c.User)
                .Where(c => c.HomeId == homeId && c.ScheduleStartAt.Year == date.Year && c.ScheduleStartAt.Month == date.Month)
                .OrderBy(c => c.Room)
                .ToListAsync();
        }

        public async Task<List<CleaningSchedule>> GetTasksWithMoreThan8daysWithoutClompleted()
        {
            var todayLess8days = DateTime.UtcNow.Date.AddDays(-8);

            return await _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.CleaningTasksCompleteds)
                .Include(c => c.User)
                .Where(c => !c.ScheduleFinishAt.HasValue && c.HomeId == c.User.HomeAssociation.HomeId)
                .Where(c => (!c.CleaningTasksCompleteds.Any() && c.ScheduleStartAt.Date < todayLess8days) || 
                            (c.CleaningTasksCompleteds.Any() && c.CleaningTasksCompleteds.OrderByDescending(k => k.CreateDate).First().CreateDate.Date < todayLess8days))
                .ToListAsync();
        }
    }
}
