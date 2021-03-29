using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleUseCase : ICreateFirstScheduleUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleWriteOnlyRepository _repository;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryCleaningScheduleReadOnly;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;

        public CreateFirstScheduleUseCase(ICleaningScheduleWriteOnlyRepository repository, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleReadOnlyRepository repositoryCleaningScheduleReadOnly, IHashids hashids,
            IPushNotificationService pushNotificationService, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _hashids = hashids;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repositoryCleaningScheduleReadOnly = repositoryCleaningScheduleReadOnly;
            _pushNotificationService = pushNotificationService;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(IList<RequestUpdateCleaningScheduleJson> request)
        {
            var loggedUser = await _loggedUser.User();
            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

            await Validate(loggedUser, request, friends);

            foreach(var friend in request)
            {
                var userId = _hashids.DecodeLong(friend.UserId).First();
                var schedules = friend.Rooms.Select(c => CreateSchedule(loggedUser.HomeAssociation.HomeId, userId, c));
                
                await _repository.Add(schedules);
            }

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            await SendNotification(friends.Where(c => c.Id != loggedUser.Id).Select(c => c.PushNotificationId).ToList());

            return response;
        }

        private async Task Validate(Domain.Entity.User loggedUser, IList<RequestUpdateCleaningScheduleJson> request, IList<Domain.Entity.User> users)
        {
            var validation = await new CreateFirstScheduleValidate(_repositoryCleaningScheduleReadOnly, loggedUser.HomeAssociation.HomeId).ValidateAsync(request);
            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            if (!request.SelectMany(c => c.Rooms).Distinct().All(c => loggedUser.HomeAssociation.Home.Rooms.Any(k => k.Name.Equals(c))))
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.ROOM_DOES_NOT_EXIST_HOME });

            if (!request.Select(c => c.UserId).All(c => users.Any(w => w.Id == _hashids.DecodeLong(c).First())))
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.INVALID_USER });
        }

        private Domain.Entity.CleaningSchedule CreateSchedule(long homeId, long userId, string room)
        {
            return new Domain.Entity.CleaningSchedule
            {
                HomeId = homeId,
                ScheduleStartAt = DateTime.UtcNow,
                UserId = userId,
                Room = room
            };
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning Schedule created 🏡" },
                { "pt", "Cronograma de limpeza criado 🏡" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "Enter in the app and check" },
                { "pt", "Entre no app e confira ;)" }
            };

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
