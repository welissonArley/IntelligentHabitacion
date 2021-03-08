using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.UpdateCleaningSchedule
{
    public class UpdateCleaningScheduleUseCase : IUpdateCleaningScheduleUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleWriteOnlyRepository _repository;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryCleaningScheduleReadOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public UpdateCleaningScheduleUseCase(ICleaningScheduleWriteOnlyRepository repository, ILoggedUser loggedUser,
            IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleReadOnlyRepository repositoryCleaningScheduleReadOnly,
            IUserReadOnlyRepository userReadOnlyRepository, IPushNotificationService pushNotificationService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repositoryCleaningScheduleReadOnly = repositoryCleaningScheduleReadOnly;
            _userReadOnlyRepository = userReadOnlyRepository;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<ResponseOutput> Execute(List<RequestUpdateCleaningScheduleJson> request)
        {
            var loggedUser = await _loggedUser.User();

            var tasksDto = _mapper.Map<List<Domain.Dto.UpdateCleaningScheduleDto>>(request);

            await Validate(request, tasksDto, loggedUser.HomeAssociation.HomeId);

            bool changedSomething = false;

            foreach (var dto in tasksDto)
            {
                var schedules = dto.Rooms.Select(c => CreateSchedule(loggedUser.HomeAssociation.HomeId, dto.UserId, c)).ToList();

                var currentSchedules = await _repositoryCleaningScheduleReadOnly.GetCurrentUserSchedules(dto.UserId, loggedUser.HomeAssociation.HomeId);

                var scheduleToFinish = currentSchedules.Where(c => !schedules.Any(k => k.Room.Equals(c.Room) && k.UserId == c.UserId)).ToList();
                var schedulesDuplicate = currentSchedules.Where(c => schedules.Any(k => k.Room.Equals(c.Room) && k.UserId == c.UserId)).ToList();

                foreach (var scheduleToRemove in schedulesDuplicate)
                    schedules.Remove(schedules.First(c => c.Room.Equals(scheduleToRemove.Room) && c.UserId == scheduleToRemove.UserId));

                if(!schedules.All(c => loggedUser.HomeAssociation.Home.Rooms.Any(k => k.Name.Equals(c.Room))))
                    throw new ErrorOnValidationException(new List<string> { ResourceTextException.ROOM_DOES_NOT_EXIST_HOME });

                await _repository.Add(schedules);

                var scheduleToFinishOnDatabase = scheduleToFinish.Select(c => c.Id).ToList();
                _repository.FinishSchedules(scheduleToFinishOnDatabase);

                if (schedules.Any() || scheduleToFinishOnDatabase.Any())
                    changedSomething = true;
            }

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            if (changedSomething)
            {
                var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);
                await SendNotification(friends.Where(c => c.Id != loggedUser.Id).Select(c => c.PushNotificationId).ToList());
            }

            return response;
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

        private async Task Validate(List<RequestUpdateCleaningScheduleJson> request, List<Domain.Dto.UpdateCleaningScheduleDto> tasksDto, long homeId)
        {
            var validation = new UpdateCleaningScheduleValidation().Validate(request);
            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            var friends = await _userReadOnlyRepository.GetByHome(homeId);

            if (!tasksDto.All(c => friends.Any(k => k.Id == c.UserId)))
                throw new YouCannotPerformThisActionException();
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning Schedule updated 🏡" },
                { "pt", "Cronograma de limpeza atualizado 🏡" }
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
