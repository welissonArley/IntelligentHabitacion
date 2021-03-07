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
            var user = await _userReadOnlyRepository.GetById(userId);

            if (user == null)
                throw new InvalidUserException();

            var loggedUser = await _loggedUser.User();

            var resultJson = _mapper.Map<ResponseDetailsUserScheduleJson>(user);
            resultJson.Month = new DateTime(date.Year, date.Month, 1).Date;

            var schedules = await _scheduleReadOnlyRepository.GetAllTasksUser(userId, loggedUser.HomeAssociation.HomeId, date);

            foreach(var schedule in schedules)
            {
                var list = schedule.CleaningTasksCompleteds.Select(async c => new ResponseTaskForTheMonthDetailsJson
                {
                    Room = schedule.Room,
                    Date = c.CreateDate,
                    AverageRating = c.AverageRating,
                    CanBeRate = userId != loggedUser.Id
                        && DateTime.Compare(resultJson.Month, new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).Date) == 0
                        && user.HomeAssociation.HomeId == loggedUser.HomeAssociation.HomeId
                        && !(await _scheduleReadOnlyRepository.UserAlreadyRatedTheTask(loggedUser.Id, c.Id)),
                    Id = _hashids.EncodeLong(c.Id)
                }).Select(c => c.Result).ToList();

                resultJson.Tasks.AddRange(list);
            }

            resultJson.Tasks = resultJson.Tasks.OrderByDescending(c => c.Date).ThenBy(c => c.Room).ToList();

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, resultJson);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
