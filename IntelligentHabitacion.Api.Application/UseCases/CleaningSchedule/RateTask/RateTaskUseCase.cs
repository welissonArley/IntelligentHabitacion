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

            var task = await _repositoryReadOnly.GetTaskByCompletedId(taskCompletedId);

            await Validate(task, loggedUser, taskCompletedId, request);

            var averageRating = await _repositoryWriteOnly.AddRateTask_ReturnAverageRating(new Domain.Entity.CleaningRating
            {
                CleaningTaskCompletedId = taskCompletedId,
                Rating = request.Rating,
                FeedBack = request.FeedBack
            }, loggedUser.Id);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, new ResponseAverageRatingJson { AverageRating = averageRating });

            await _unitOfWork.Commit();

            await SendNotification(task.Room, task.User.PushNotificationId);

            return response;
        }

        private async Task Validate(Domain.Entity.CleaningSchedule task, Domain.Entity.User userLogged, long taskCompletedId, RequestRateTaskJson request)
        {
            if (task == null)
                throw new InvalidTaskException();

            if(!(request.Rating >= 0 && request.Rating <= 5))
                throw new InvalidRatingException();

            if (task.UserId == userLogged.Id)
                throw new UserRatingOwnTaskException();

            if (task.User.HomeAssociation.HomeId != userLogged.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (DateTime.Compare(new DateTime(task.ScheduleStartAt.Year, task.ScheduleStartAt.Month, 1).Date, new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).Date) != 0)
                throw new InvalidDateToRateException();

            var userAlreadyRatedTheTask = await _repositoryReadOnly.UserAlreadyRatedTheTask(userLogged.Id, taskCompletedId);
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
