using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeDateFriendJoinHome
{
    public class ChangeDateFriendJoinHomeUseCase : IChangeDateFriendJoinHomeUseCase
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserUpdateOnlyRepository _repository;

        public ChangeDateFriendJoinHomeUseCase(IUserUpdateOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser,
            IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long myFriendId, RequestChangeDateJoinHomeJson request)
        {
            var loggedUser = await _loggedUser.User();
            var friend = await _repository.GetById_Update(myFriendId);

            Validate(friend, loggedUser, request.JoinOn);

            friend.HomeAssociation.JoinedOn = request.JoinOn.Date;

            _repository.Update(friend);

            var converter = new DateStringConverter();

            var resultJson = _mapper.Map<ResponseFriendJson>(friend);
            resultJson.DescriptionDateJoined = string.Format(ResourceText.DESCRIPTION_DATE_JOINED_THE_HOUSE, converter.DateToStringYearMonthAndDay(loggedUser.HomeAssociation.JoinedOn));

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, resultJson);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(Domain.Entity.User friend, Domain.Entity.User loggedUser, DateTime newDate)
        {
            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.Home.AdministratorId != loggedUser.Id)
                throw new YouCannotPerformThisActionException();

            if (DateTime.Compare(newDate.Date, friend.HomeAssociation.JoinedOn.Date) > 0)
                throw new InvalidDateException(friend.HomeAssociation.JoinedOn);
        }
    }
}
