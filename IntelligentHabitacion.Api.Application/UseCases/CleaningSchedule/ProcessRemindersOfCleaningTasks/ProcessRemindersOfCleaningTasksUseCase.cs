using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks
{
    public class ProcessRemindersOfCleaningTasksUseCase : IProcessRemindersOfCleaningTasksUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ICleaningScheduleReadOnlyRepository _repository;

        public ProcessRemindersOfCleaningTasksUseCase(IPushNotificationService pushNotificationService,
            ICleaningScheduleReadOnlyRepository repository)
        {
            _pushNotificationService = pushNotificationService;
            _repository = repository;
        }

        public async Task Execute()
        {
            var query = await _repository.GetTasksWithMoreThan8daysWithoutClompleted();

            var group = query.GroupBy(c => c.UserId);

            var users = query.Select(c => c.User).Distinct();

            if (query.Count() > 0)
            {
                foreach (var result in group)
                {
                    var schedules = result.ToList();
                    await ProcessSchedules(users.First(c => c.Id == result.Key), schedules);
                }
            }
        }

        private async Task ProcessSchedules(Domain.Entity.User user, List<Domain.Entity.CleaningSchedule> schedules)
        {
            var today = DateTime.UtcNow.Date;
            Dictionary<string, string> titles;
            Dictionary<string, string> messages;

            foreach (var task in schedules)
            {
                var totalDays = (today - (task.CleaningTasksCompleteds.Any() ? task.CleaningTasksCompleteds.OrderByDescending(c => c.CreateDate).First().CreateDate : task.ScheduleStartAt)).TotalDays;

                titles = new Dictionary<string, string>
                {
                    { "en", $"Cleaning reminder {task.Room.ToUpper()} ⌛" },
                    { "pt", $"Lembrete de limpeza {task.Room.ToUpper()} ⌛" }
                };
                messages = new Dictionary<string, string>
                {
                    { "en", $"Hey, it's been {totalDays} days since you cleaned this room. You can do it, help keep everything organized  🥰 " },
                    { "pt", $"Hey, já fazem {totalDays} dias que você não limpou este cômodo. Você consegue, ajude a manter tudo organizado  🥰 " }
                };

                await SendNotification(user, titles, messages);
            }
        }

        private async Task SendNotification(Domain.Entity.User user, Dictionary<string, string> titles, Dictionary<string, string> messages)
        {
            var random = new Random();

            var today = DateTime.Today.Date;
            var ts = new TimeSpan(random.Next(7, 11), random.Next(0, 59), 0);

            await _pushNotificationService.Send(titles, messages, new List<string> { user.PushNotificationId }, today + ts);
        }
    }
}
