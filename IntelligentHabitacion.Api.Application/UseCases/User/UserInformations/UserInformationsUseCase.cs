using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.UserInformations
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

        public async Task<ResponseOutput> Execute()
        {
            var user = await _loggedUser.User();

            var json = _mapper.Map<ResponseUserInformationsJson>(user);

            var response = await _intelligentHabitacionUseCase.CreateResponse(user.Email, user.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
