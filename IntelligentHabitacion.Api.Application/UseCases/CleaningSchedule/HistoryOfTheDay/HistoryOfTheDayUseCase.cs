using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Dto;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public class HistoryOfTheDayUseCase : IHistoryOfTheDayUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;

        public HistoryOfTheDayUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            IHashids hashids)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(RequestHistoryOfTheDayJson request)
        {
            var loggedUser = await _loggedUser.User();
            
            var history = await _repository.GetHistoryOfTheDay(request.Date, loggedUser.HomeAssociation.HomeId, request.Room, loggedUser.Id);

            var responseJson = Mapper(history);
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private IList<ResponseHistoryOfTheDayJson> Mapper(IList<CleaningScheduleHistoryOfTheDayDto> dto)
        {
            return dto.Select(c => new ResponseHistoryOfTheDayJson
            {
                User = c.User,
                AverageRate = c.AverageRate,
                CanRate = c.CanRate,
                Id = _hashids.EncodeLong(c.Id)
            }).ToList();
        }
    }
}
