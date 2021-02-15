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

        public RegisterUserUseCase(IMapper mapper, IUnitOfWork unitOfWork, IUserWriteOnlyRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public string Execute(RequestRegisterUserJson registerUserJson)
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

                return userModel.ProfileColor;
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
