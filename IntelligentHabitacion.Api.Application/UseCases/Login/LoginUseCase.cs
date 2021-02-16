using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.ExceptionsBase;

namespace IntelligentHabitacion.Api.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly PasswordEncripter _cryptography;
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter cryptography,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cryptography = cryptography;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ResponseOutput Execute(RequestLoginJson loginJson)
        {
            var user = _repository.GetByEmailPassword(loginJson.User, _cryptography.Encrypt(loginJson.Password));

            if (user == null)
                throw new InvalidLoginException();

            var json = _mapper.Map<ResponseLoginJson>(user);
            var response = _intelligentHabitacionUseCase.CreateResponse(user.Email, user.Id, json);
            _unitOfWork.Commit();

            return response;
        }
    }
}
