using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.DetailsAllRate
{
    public class DetailsAllRateUseCase : IDetailsAllRateUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DetailsAllRateUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(long completedTaskId)
        {
            var loggedUser = await _loggedUser.User();

            var cleaningTasksCompleted = await _repository.GetTaskCompletedById(completedTaskId);

            var responseJson = Mapper(cleaningTasksCompleted);
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private List<ResponseRateTaskJson> Mapper(Domain.Entity.CleaningTasksCompleted cleaningTasksCompleted)
        {
            return cleaningTasksCompleted.Ratings.Select(c => new ResponseRateTaskJson
            {
                Date = cleaningTasksCompleted.CreateDate,
                Feedback = c.FeedBack,
                Name = cleaningTasksCompleted.CleaningSchedule.User.Name,
                Room = cleaningTasksCompleted.CleaningSchedule.Room,
                Rating = c.Rating
            }).ToList();
        }
    }
}
