using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.GetCleaningSchedule
{
    public class GetCleaningScheduleUseCase : IGetCleaningScheduleUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GetCleaningScheduleUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(DateTime date)
        {
            var loggedUser = await _loggedUser.User();

            if (!loggedUser.HomeAssociation.Home.Rooms.Any())
            {
                return await CreateResponse(loggedUser, new ResponseNeedActionJson
                {
                    Action = Communication.Enums.NeedActionEnum.RegisterRoom,
                    Message = ResourceText.MESSAGE_REGISTER_ROOM
                });
            }

            var homeHasCleaningSchedule = await _repository.HomeHasCleaningScheduleCreated(loggedUser.HomeAssociation.HomeId);

            if (!homeHasCleaningSchedule)
            {
                return await CreateResponse(loggedUser, new ResponseNeedActionJson
                {
                    Action = Communication.Enums.NeedActionEnum.CreateTheCleaningSchedule,
                    Message = ResourceText.DESCRIPTION_CREATE_CLEANING_SCHEDULE
                });
            }

            var cleaningSchedules = await _repository.GetTasksUser(loggedUser.Id, loggedUser.HomeAssociation.HomeId, date);

            var json = new ResponseMyTasksCleaningScheduleJson
            {
                Month = date,
                Tasks = _mapper.Map<List<ResponseTasksForTheMonthJson>>(cleaningSchedules)
            };

            return await CreateResponse(loggedUser, json);
        }

        private async Task<ResponseOutput> CreateResponse(Domain.Entity.User loggedUser, object json)
        {
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
