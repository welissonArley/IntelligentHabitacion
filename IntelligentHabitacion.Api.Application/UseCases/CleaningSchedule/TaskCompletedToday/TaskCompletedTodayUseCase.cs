using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Exception.API;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.TaskCompletedToday
{
    public class TaskCompletedTodayUseCase : ITaskCompletedTodayUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICleaningScheduleWriteOnlyRepository _repositoryWriteOnly;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryReadOnly;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public TaskCompletedTodayUseCase(IPushNotificationService pushNotificationService, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleWriteOnlyRepository repositoryWriteOnly, IUserReadOnlyRepository userReadOnlyRepository,
            ICleaningScheduleReadOnlyRepository repositoryReadOnly)
        {
            _pushNotificationService = pushNotificationService;
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repositoryWriteOnly = repositoryWriteOnly;
            _repositoryReadOnly = repositoryReadOnly;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(long taskId)
        {
            var loggedUser = await _loggedUser.User();

            var task = await _repositoryReadOnly.GetTaskById(taskId, loggedUser.Id, loggedUser.HomeAssociation.HomeId);
            if (task == null)
                throw new InvalidTaskException();

            await _repositoryWriteOnly.CompletedTask(task.Id);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            var friends = (await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId)).Where(c => c.Id != loggedUser.Id);

            SendNotification(loggedUser.Name, task.Room, friends.Select(c => c.PushNotificationId).ToList());

            return response;
        }

        private void SendNotification(string userName, string room, List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Clean room 💦" },
                { "pt", "Cômodo limpo 💦" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", string.Format("{0} cleaned the {1}, uhuu. You can now go to the App and rate the task ✔️", userName, room) },
                { "pt", string.Format("{0} limpou a(o) {1}, uhuu. Você já pode ir no App e avaliar a tarefa ✔️", userName, room) }
            };

            _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
