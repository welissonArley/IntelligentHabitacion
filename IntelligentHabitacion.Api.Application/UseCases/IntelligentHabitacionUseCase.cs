using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Repository.Token;

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

        public ResponseOutput CreateResponse(string email, long userId, object response)
        {
            return new ResponseOutput
            {
                Token = GenerateNewToken(email, userId),
                ResponseJson = response
            };
        }

        private string GenerateNewToken(string email, long userId)
        {
            var token = _tokenController.Generate(email);

            _tokenRepository.Add(new Domain.Entity.Token
            {
                Value = token,
                UserId = userId
            });

            return token;
        }
    }
}
