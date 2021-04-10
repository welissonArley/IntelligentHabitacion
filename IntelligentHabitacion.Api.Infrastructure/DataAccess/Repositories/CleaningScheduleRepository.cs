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

        public async Task<bool> HomeHasCleaningScheduleCreated(long homeId)
        {
            return await _context.CleaningSchedules.AnyAsync(c => c.HomeId == homeId);
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

        public async Task<List<CleaningSchedule>> GetTasksForTheMonth(DateTime month, long homeId)
        {
            return await _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.ScheduleStartAt.Month == month.Month && c.ScheduleStartAt.Year == month.Year &&
                    !c.ScheduleFinishAt.HasValue && c.HomeId == homeId)
                .ToListAsync();
        }

        public async Task<bool> TaskCleanedOnDate(long taskId, DateTime date)
        {
            return await _context.CleaningTasksCompleteds.AnyAsync(c => c.CleaningScheduleId == taskId &&
                c.CreateDate.Date == date.Date);
        }

        public async Task<bool> ThereAreaTaskToUserRateThisMonth(long userId, string room)
        {
            var today = DateTime.UtcNow;

            var taskIds = _context.CleaningSchedules.Where(c => c.ScheduleStartAt.Year == today.Year && c.ScheduleStartAt.Month == today.Month && !c.ScheduleFinishAt.HasValue && c.Room.Equals(room) && c.UserId != userId).Select(c => c.Id);
            var tasksCompletedsIds = _context.CleaningTasksCompleteds.Where(c => taskIds.Contains(c.CleaningScheduleId)).Select(c => c.Id);
            if(await tasksCompletedsIds.AnyAsync())
            {
                return await _context.CleaningRatingUsers.AllAsync(c => c.UserId != userId && tasksCompletedsIds.Contains(c.CleaningTaskCompletedId));
            }

            return false;
        }

        public async Task FinishAllFromTheUser(long userId, long homeId)
        {
            var schedules = _context.CleaningSchedules.Where(c => c.UserId == userId && c.HomeId == homeId);
            foreach (var schedule in schedules)
                schedule.ScheduleFinishAt = DateTime.UtcNow;

            if (await schedules.AnyAsync())
                _context.CleaningSchedules.UpdateRange(schedules);
        }

        public async Task RegisterRoomCleaned(long taskId, DateTime date)
        {
            await _context.CleaningTasksCompleteds.AddAsync(new CleaningTasksCompleted
            {
                CleaningScheduleId = taskId,
                CreateDate = date
            });
        }

        public Task<CleaningSchedule> GetTaskById(long id)
        {
            return _context.CleaningSchedules.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IList<CleaningScheduleCalendarDayInfoDto>> GetCalendarTasksForMonth(DateTime month, long homeId, string room, long userId)
        {
            var query = _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.CleaningTasksCompleteds)
                .Where(c => c.CleaningTasksCompleteds.Any() && c.HomeId == homeId && c.ScheduleStartAt.Month == month.Month && c.ScheduleStartAt.Year == month.Year);

            if (!string.IsNullOrWhiteSpace(room))
                query = query.Where(c => c.Room.Equals(room));

            var result = await query.SelectMany(c => c.CleaningTasksCompleteds).ToListAsync();
            var group = result.GroupBy(c => c.CreateDate.Day).OrderBy(c => c.Key);

            var response = new List<CleaningScheduleCalendarDayInfoDto>();

            foreach(var dayTask in group)
            {
                response.Add(new CleaningScheduleCalendarDayInfoDto
                {
                    Day = dayTask.Key,
                    AmountCleanedRecords = dayTask.Count(),
                    AmountcleanedRecordsToRate = dayTask.Count(c => CountAmountcleanedRecordsToRate(userId, c, query))
                });
            }

            return response;
        }

        private bool CountAmountcleanedRecordsToRate(long userId, CleaningTasksCompleted cleaningTasksCompleted, IQueryable<CleaningSchedule> cleaningSchedules)
        {
            return cleaningSchedules.First(c => c.Id == cleaningTasksCompleted.CleaningScheduleId).UserId == userId ? false :
                !_context.CleaningRatingUsers.Any(w => w.UserId == userId && w.CleaningTaskCompletedId == cleaningTasksCompleted.Id);
        }

        public async Task<IList<CleaningScheduleHistoryRoomOfTheDayDto>> GetHistoryOfTheDay(DateTime date, long homeId, string room, long userId)
        {
            var query = _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.CleaningTasksCompleteds)
                .Where(c => c.CleaningTasksCompleteds.Any() && c.HomeId == homeId && c.ScheduleStartAt.Month == date.Month && c.ScheduleStartAt.Year == date.Year);

            if (!string.IsNullOrWhiteSpace(room))
                query = query.Where(c => c.Room.Equals(room));

            var resultQuery = await query.ToListAsync();

            var response = new List<CleaningScheduleHistoryRoomOfTheDayDto>();

            IEnumerable<CleaningTasksCompleted> list;

            foreach (var cleaningSchedule in resultQuery)
            {
                list = cleaningSchedule.CleaningTasksCompleteds.Where(c => c.CreateDate.Date == date.Date);

                var dtoList = list.Select(c => new CleaningScheduleHistoryCleanDayDto
                {
                    Id = c.Id,
                    AverageRate = c.AverageRating,
                    User = cleaningSchedule.User.Name,
                    CanRate = cleaningSchedule.UserId != userId
                });

                foreach (var task in dtoList.Where(c => c.CanRate))
                    task.CanRate = !(await _context.CleaningRatingUsers.AnyAsync(w => w.UserId == userId && w.CleaningTaskCompletedId == task.Id));

                if (dtoList.Any())
                {
                    if (response.Any(c => c.Room.Equals(cleaningSchedule.Room)))
                        response.First(c => c.Room.Equals(cleaningSchedule.Room)).History.AddRange(dtoList);
                    else
                    {
                        response.Add(new CleaningScheduleHistoryRoomOfTheDayDto
                        {
                            Room = cleaningSchedule.Room,
                            History = dtoList.ToList()
                        });
                    }
                }
            }

            return response.OrderBy(c => c.Room).ToList();
        }

        public async Task<IList<CleaningSchedule>> GetScheduleRoomForCurrentMonth(long homeId, string room)
        {
            var today = DateTime.Today;

            return await _context.CleaningSchedules.AsNoTracking()
                .Include(c => c.CleaningTasksCompleteds)
                .Where(c => c.ScheduleStartAt.Month == today.Month && c.ScheduleStartAt.Year == today.Year &&
                    c.Room.Equals(room) && c.HomeId == homeId).ToListAsync();
        }

        public void Remove(CleaningSchedule schedule)
        {
            _context.CleaningSchedules.Remove(schedule);
        }

        public async Task FinishTask(long taskId)
        {
            var schedule = await _context.CleaningSchedules.FirstOrDefaultAsync(c => c.Id == taskId);
            if(schedule != null)
            {
                schedule.ScheduleFinishAt = DateTime.UtcNow;

                _context.CleaningSchedules.Update(schedule);
            }
        }
    }
}
