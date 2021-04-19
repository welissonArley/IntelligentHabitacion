using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Boolean;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredUseCase : IEmailAlreadyBeenRegisteredUseCase
    {
        private readonly IUserReadOnlyRepository _repository;

        public EmailAlreadyBeenRegisteredUseCase(IUserReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<BooleanJson> Execute(string email)
        {
            return new BooleanJson
            {
                Value = await _repository.ExistActiveUserWithEmail(email)
            };
        }
    }
}
