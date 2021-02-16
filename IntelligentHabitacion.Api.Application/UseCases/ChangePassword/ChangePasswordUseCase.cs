using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly PasswordEncripter _cryptography;

        public ChangePasswordUseCase(ILoggedUser loggedUser,
            IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, PasswordEncripter cryptography,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _cryptography = cryptography;
        }

        public ResponseOutput Execute(RequestChangePasswordJson changePasswordJson)
        {
            var loggedUser = _loggedUser.User();

            Validate(changePasswordJson, loggedUser);

            var userToUpdate = _repository.GetById_Update(loggedUser.Id);
            userToUpdate.Password = _cryptography.Encrypt(changePasswordJson.NewPassword);

            _repository.Update(userToUpdate);
            var response = _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            _unitOfWork.Commit();

            return response;
        }

        private void Validate(RequestChangePasswordJson changePasswordJson, Domain.Entity.User userDataNow)
        {
            var validation = new ChangePasswordValidation(_cryptography, userDataNow).Validate(changePasswordJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
