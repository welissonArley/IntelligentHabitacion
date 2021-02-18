using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IEmailAlreadyBeenRegisteredUseCase _registeredUseCase;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly PasswordEncripter _cryptography;

        public RegisterUserUseCase(IMapper mapper, IUnitOfWork unitOfWork,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUserWriteOnlyRepository repository,
            IEmailAlreadyBeenRegisteredUseCase registeredUseCase, PasswordEncripter cryptography)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _repository = repository;
            _registeredUseCase = registeredUseCase;
            _cryptography = cryptography;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterUserJson registerUserJson)
        {
            var validation = new RegisterUserValidation(_registeredUseCase).Validate(registerUserJson);

            if (validation.IsValid)
            {
                var userModel = _mapper.Map<Domain.Entity.User>(registerUserJson);
                userModel.Password = _cryptography.Encrypt(userModel.Password);

                await _repository.Add(userModel);
                await _unitOfWork.Commit();

                var response = await _intelligentHabitacionUseCase.CreateResponse(userModel.Email, userModel.Id, userModel.ProfileColor);
                
                await _unitOfWork.Commit();

                return response;
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
