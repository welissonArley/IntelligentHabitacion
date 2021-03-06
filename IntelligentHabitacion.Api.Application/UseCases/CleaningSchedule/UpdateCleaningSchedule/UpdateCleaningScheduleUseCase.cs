using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.UpdateCleaningSchedule
{
    public class UpdateCleaningScheduleUseCase : IUpdateCleaningScheduleUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleWriteOnlyRepository _repository;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryCleaningScheduleReadOnly;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCleaningScheduleUseCase(ICleaningScheduleWriteOnlyRepository repository, ILoggedUser loggedUser,
            IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleReadOnlyRepository repositoryCleaningScheduleReadOnly)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repositoryCleaningScheduleReadOnly = repositoryCleaningScheduleReadOnly;
        }

        public async Task<ResponseOutput> Execute(List<RequestUpdateCleaningScheduleJson> request)
        {
            var validation = new UpdateCleaningScheduleValidation().Validate(request);
            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            var loggedUser = await _loggedUser.User();

            var tasksDto = _mapper.Map<List<Domain.Dto.UpdateCleaningScheduleDto>>(request);

            foreach(var dto in tasksDto)
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
                _repository.FinishSchedules(scheduleToFinish.Select(c => c.Id).ToList());
            }

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

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
    }
}
