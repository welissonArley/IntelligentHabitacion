using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterHome
{
    public class RegisterHomeUseCase : IRegisterHomeUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHomeUseCase(IUserUpdateOnlyRepository userRepository, IUnitOfWork unitOfWork,
            ILoggedUser loggedUser, IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterHomeJson registerHomeJson)
        {
            var loggedUser = await _loggedUser.User();
            Validate(loggedUser, registerHomeJson);

            var homeModel = _mapper.Map<Home>(registerHomeJson);
            homeModel.AdministratorId = loggedUser.Id;

            var userToUpdate = await _userRepository.GetById_Update(loggedUser.Id);
            userToUpdate.HomeAssociation = new HomeAssociation
            {
                JoinedOn = DateTime.UtcNow,
                Home = homeModel
            };
            _userRepository.Update(userToUpdate);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(User loggedUser, RequestRegisterHomeJson registerHomeJson)
        {
            if (loggedUser.HomeAssociationId != null)
                throw new UserIsPartOfAHomeException();

            var validation = new RegisterHomeValidation().Validate(registerHomeJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
