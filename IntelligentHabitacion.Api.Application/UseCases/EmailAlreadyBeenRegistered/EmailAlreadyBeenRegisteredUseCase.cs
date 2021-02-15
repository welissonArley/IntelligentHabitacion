using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Boolean;

namespace IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredUseCase : IEmailAlreadyBeenRegisteredUseCase
    {
        private readonly IUserReadOnlyRepository _repository;

        public EmailAlreadyBeenRegisteredUseCase(IUserReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public BooleanJson Execute(string email)
        {
            return new BooleanJson
            {
                Value = _repository.ExistActiveUserWithEmail(email)
            };
        }
    }
}
