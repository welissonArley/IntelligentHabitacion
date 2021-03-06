using AutoMapper;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.GetFriendsTasks
{
    public class GetFriendsTasksUseCase : IGetFriendsTasksUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IHashids _hashids;

        public GetFriendsTasksUseCase(IntelligentHabitacionUseCase intelligentHabitacionUseCase, IMapper mapper,
            ILoggedUser loggedUser, ICleaningScheduleReadOnlyRepository repository, IUnitOfWork unitOfWork,
            IUserReadOnlyRepository userReadOnlyRepository, IHashids hashids)
        {
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(DateTime date)
        {
            var loggedUser = await _loggedUser.User();

            var friends = (await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId)).Where(c => c.Id != loggedUser.Id);

            var responseJson = new List<ResponseAllFriendsTasksScheduleJson>();

            foreach(var friend in friends)
            {
                var schedule = await _repository.GetAllTasksUser(friend.Id, friend.HomeAssociation.HomeId, date);

                var userResponse = _mapper.Map<ResponseAllFriendsTasksScheduleJson>(friend);
                userResponse.Tasks.AddRange(schedule.Select(c => new ResponseTasksForTheMonthJson
                {
                    Id = _hashids.EncodeLong(c.Id),
                    Room = c.Room,
                    CleaningRecords = c.CleaningTasksCompleteds.Count
                }));

                responseJson.Add(userResponse);
            }

            SortList(responseJson);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private void SortList(List<ResponseAllFriendsTasksScheduleJson> list)
        {
            var friendsWithoutTask = list.Where(c => !c.Tasks.Any()).ToList();

            foreach(var friend in friendsWithoutTask)
                list.Remove(friend);

            list = list.OrderByDescending(c => c.Tasks.Count).ThenBy(c => c.Name).ToList();
            friendsWithoutTask = friendsWithoutTask.OrderByDescending(c => c.Name).ToList();

            foreach (var friend in friendsWithoutTask)
                list.Insert(0, friend);
        }
    }
}
