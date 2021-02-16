using AutoMapper;
using IntelligentHabitacion.Api.Application.Services;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Communication.Response;

namespace IntelligentHabitacion.Api.Application.UseCases.UserInformations
{
    public class UserInformationsUseCase : IUserInformationsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public UserInformationsUseCase(ILoggedUser loggedUser, IMapper mapper,
            IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseOutput Execute()
        {
            var user = _loggedUser.User();

            var json = _mapper.Map<ResponseUserInformationsJson>(user);

            var response = _intelligentHabitacionUseCase.CreateResponse(user.Email, user.Id, json);

            _unitOfWork.Commit();

            return response;
        }
    }
}
