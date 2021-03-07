using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetTaskFeedbacks
{
    public class GetTaskFeedbacksUseCase : IGetTaskFeedbacksUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskFeedbacksUseCase(IntelligentHabitacionUseCase intelligentHabitacionUseCase, IMapper mapper,
            ILoggedUser loggedUser, ICleaningScheduleReadOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(long taskCompletedId)
        {
            var loggedUser = await _loggedUser.User();

            var task = await _repository.GetTaskByCompletedId(taskCompletedId);

            if (task.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            var ratesList = await _repository.GetRates(taskCompletedId);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id,
                new ResponseRateTaskJson
                {
                    CleanedBy = task.User.Name,
                    Room = task.Room,
                    CleanedAt = task.CleaningTasksCompleteds.First(c => c.Id == taskCompletedId).CreateDate,
                    Rates = _mapper.Map<List<ResponseRateJson>>(ratesList)
                });

            await _unitOfWork.Commit();

            return response;
        }
    }
}
