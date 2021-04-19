using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.RateTask
{
    public class RateTaskUseCase : IRateTaskUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICleaningScheduleWriteOnlyRepository _repositoryWriteOnly;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryReadOnly;

        public RateTaskUseCase(IPushNotificationService pushNotificationService, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleWriteOnlyRepository repositoryWriteOnly,
            ICleaningScheduleReadOnlyRepository repositoryReadOnly)
        {
            _pushNotificationService = pushNotificationService;
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repositoryWriteOnly = repositoryWriteOnly;
            _repositoryReadOnly = repositoryReadOnly;
        }

        public async Task<ResponseOutput> Execute(long taskCompletedId, RequestRateTaskJson request)
        {
            var loggedUser = await _loggedUser.User();

            var task = await _repositoryReadOnly.GetTaskCompletedById(taskCompletedId);

            await Validate(task, loggedUser, request);

            var averageRating = await _repositoryWriteOnly.AddRateTask_ReturnAverageRating(
                new Domain.Entity.CleaningRating
                {
                    CleaningTaskCompletedId = taskCompletedId,
                    Rating = request.Rating,
                    FeedBack = request.FeedBack
                }, loggedUser.Id);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, new ResponseAverageRatingJson { AverageRating = averageRating });

            await _unitOfWork.Commit();

            await SendNotification(task.CleaningSchedule.Room, task.CleaningSchedule.User.PushNotificationId);

            return response;
        }

        private async Task Validate(Domain.Entity.CleaningTasksCompleted task, Domain.Entity.User userLogged, RequestRateTaskJson request)
        {
            if (task == null)
                throw new InvalidTaskException();

            if (!(request.Rating >= 0 && request.Rating <= 5))
                throw new InvalidRatingException();

            if (task.CleaningSchedule.UserId == userLogged.Id)
                throw new UserRatingOwnTaskException();

            if (task.CleaningSchedule.User.HomeAssociation.HomeId != userLogged.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (!(task.CleaningSchedule.ScheduleStartAt.Year == DateTime.UtcNow.Year && task.CleaningSchedule.ScheduleStartAt.Month == DateTime.UtcNow.Month))
                throw new InvalidDateToRateException();

            var userAlreadyRatedTheTask = await _repositoryReadOnly.UserAlreadyRatedTheTask(userLogged.Id, task.Id);
            if (userAlreadyRatedTheTask)
                throw new UserAlreadyRateTaskException();
        }

        private async Task SendNotification(string room, string pushNotificationId)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning task rated 🌟" },
                { "pt", "Tarefa de limpeza avaliada 🌟" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", string.Format("Your cleaning task ({0}) has been rated :) Enter the app and check ✔️", room) },
                { "pt", string.Format("Sua tarefa de limpeza ({0}) foi avaliada :) Entre no app e confira ✔️", room) }
            };

            await _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId });
        }
    }
}
