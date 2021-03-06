using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword
{
    public class ResetPasswordUseCase : IResetPasswordUseCase
    {
        private readonly PasswordEncripter _cryptography;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly ICodeReadOnlyRepository _codeRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICodeWriteOnlyRepository _codeWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordUseCase(PasswordEncripter cryptography, IUserUpdateOnlyRepository repository,
            ICodeReadOnlyRepository codeRepository, IUserReadOnlyRepository userReadOnlyRepository,
            ICodeWriteOnlyRepository codeWriteOnlyRepository, IUnitOfWork unitOfWork)
        {
            _cryptography = cryptography;
            _repository = repository;
            _codeRepository = codeRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _codeWriteOnlyRepository = codeWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestResetYourPasswordJson resetYourPasswordJson)
        {
            var validation = new ForgotPasswordValidation(_codeRepository, _userReadOnlyRepository).Validate(resetYourPasswordJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            var user = await _repository.GetByEmail_Update(resetYourPasswordJson.Email);
            user.Password = _cryptography.Encrypt(resetYourPasswordJson.Password);

            _repository.Update(user);
            _codeWriteOnlyRepository.DeleteAllFromTheUser(user.Id);

            await _unitOfWork.Commit();
        }
    }
}
