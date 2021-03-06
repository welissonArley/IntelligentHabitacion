using AutoMapper;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetUsersTaskDetails
{
    public class GetUsersTaskDetailsUseCase : IGetUsersTaskDetailsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICleaningScheduleReadOnlyRepository _scheduleReadOnlyRepository;
        private readonly IHashids _hashids;

        public GetUsersTaskDetailsUseCase(IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            ILoggedUser loggedUser, IUnitOfWork unitOfWork, IUserReadOnlyRepository userReadOnlyRepository,
            ICleaningScheduleReadOnlyRepository scheduleReadOnlyRepository, IHashids hashids)
        {
            _mapper = mapper;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _scheduleReadOnlyRepository = scheduleReadOnlyRepository;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(long userId, DateTime date)
        {
            var loggedUser = await _loggedUser.User();

            var user = await _userReadOnlyRepository.GetById(userId);

            Validate(loggedUser, user);

            var resultJson = _mapper.Map<ResponseDetailsUserScheduleJson>(user);
            resultJson.Month = new DateTime(date.Year, date.Month, 1).Date;

            var schedules = await _scheduleReadOnlyRepository.GetAllTasksUser(userId, loggedUser.HomeAssociation.HomeId, date);

            foreach(var schedule in schedules)
            {
                resultJson.Tasks = resultJson.Tasks.Concat(schedule.CleaningTasksCompleteds.Select(c => new ResponseTaskForTheMonthDetailsJson
                {
                    Room = schedule.Room,
                    Date = c.CreateDate,
                    AverageRating = c.AverageRating,
                    CanBeRate = schedule.UserId != loggedUser.Id && DateTime.Compare(resultJson.Month, new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).Date) == 0,
                    Id = _hashids.EncodeLong(c.Id)
                })).ToList();
            }

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, resultJson);

            await _unitOfWork.Commit();

            return response;
        }

        public void Validate(Domain.Entity.User loggedUser, Domain.Entity.User userToResponse)
        {
            if (userToResponse == null || loggedUser.HomeAssociation.HomeId != userToResponse.HomeAssociation.HomeId)
                throw new InvalidUserException();
        }
    }
}
