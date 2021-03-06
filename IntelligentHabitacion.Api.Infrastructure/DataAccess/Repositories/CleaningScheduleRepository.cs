﻿using IntelligentHabitacion.Api.Domain.Dto;
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
    }
}
