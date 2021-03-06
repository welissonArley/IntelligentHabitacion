using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetCleaningSchedule
{
    public class GetCleaningScheduleUseCase : IGetCleaningScheduleUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetCleaningScheduleUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork,
            IUserReadOnlyRepository userRepository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ResponseOutput> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var schedules = await _repository.GetCurrentScheduleForHome(loggedUser.HomeAssociation.HomeId);

            var responseJson = new ResponseManageScheduleJson
            {
                RoomsAvaliables = _mapper.Map<List<ResponseRoomJson>>(loggedUser.HomeAssociation.Home.Rooms.Where(c => !schedules.Any(k => k.Room.Equals(c.Name))))
            };

            var usersIdsWithTasks = schedules.Select(c => c.UserId).Distinct();

            foreach(var userId in usersIdsWithTasks)
            {
                var usersTasksJson = _mapper.Map<ResponseAllFriendsTasksScheduleJson>(schedules.Select(c => c.User).First(c => c.Id == userId));
                usersTasksJson.Tasks = _mapper.Map<List<ResponseTasksForTheMonthJson>>(schedules.Where(c => c.UserId == userId));

                responseJson.UserTasks.Add(usersTasksJson);
            }

            var userAtHome = await _userRepository.GetByHome(loggedUser.HomeAssociation.HomeId);
            var usersWithoutTasks = _mapper.Map<List<ResponseAllFriendsTasksScheduleJson>>(userAtHome.Where(c => !usersIdsWithTasks.Any(k => k == c.Id)));
            responseJson.UserTasks = responseJson.UserTasks.Concat(usersWithoutTasks).ToList();

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
