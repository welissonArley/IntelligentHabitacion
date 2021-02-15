using AutoMapper;
using IntelligentHabitacion.Api.Application.Services;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;

        public RegisterUserUseCase(IMapper mapper, IUnitOfWork unitOfWork,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUserWriteOnlyRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _repository = repository;
        }

        public ResponseOutput Execute(RequestRegisterUserJson registerUserJson)
        {
            var validation = new RegisterUserValidation().Validate(registerUserJson);

            if (validation.IsValid)
            {
                /*if (EmailAlreadyBeenRegistered(registerUserJson.Email).Value)
                    throw new EmailAlreadyBeenRegisteredException();*/

                var userModel = _mapper.Map<Domain.Entity.User>(registerUserJson);
                //userModel.Password = _cryptography.Encrypt(userModel.Password);

                _repository.Add(userModel);
                _unitOfWork.Commit();

                var response = _intelligentHabitacionUseCase.CreateResponse(userModel.Email, userModel.Id, userModel.ProfileColor);
                
                _unitOfWork.Commit();

                return response;
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
