using IntelligentHabitacion.App.Services.Communication.User;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Exception;
using Refit;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredUseCase : UseCaseBase, IEmailAlreadyBeenRegisteredUseCase
    {
        private readonly IUserRestService _restService;

        public EmailAlreadyBeenRegisteredUseCase() : base("User")
        {
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task Execute(string email)
        {
            ValidateEmail(email);

            var response = await _restService.EmailAlreadyBeenRegistered(email, GetLanguage());

            if (response.Value)
                throw new EmailAlreadyBeenRegisteredException();
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }
    }
}
