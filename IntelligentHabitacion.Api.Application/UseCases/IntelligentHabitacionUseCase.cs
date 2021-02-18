using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases
{
    public class IntelligentHabitacionUseCase
    {
        private readonly ITokenWriteOnlyRepository _tokenRepository;
        private readonly TokenController _tokenController;

        public IntelligentHabitacionUseCase(TokenController tokenController, ITokenWriteOnlyRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _tokenController = tokenController;
        }

        public async Task<ResponseOutput> CreateResponse(string email, long userId, object response = null)
        {
            return new ResponseOutput
            {
                Token = await GenerateNewToken(email, userId),
                ResponseJson = response
            };
        }

        private async Task<string> GenerateNewToken(string email, long userId)
        {
            var token = _tokenController.Generate(email);

            await _tokenRepository.Add(new Domain.Entity.Token
            {
                Value = token,
                UserId = userId
            });

            return token;
        }
    }
}
