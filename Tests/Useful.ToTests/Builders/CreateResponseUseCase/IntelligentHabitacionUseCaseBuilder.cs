using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using Useful.ToTests.Builders.Repositories;

namespace Useful.ToTests.Builders.CreateResponseUseCase
{
    public class IntelligentHabitacionUseCaseBuilder
    {
        private static IntelligentHabitacionUseCaseBuilder _instance;
        private readonly ITokenWriteOnlyRepository _tokenRepository;

        private IntelligentHabitacionUseCaseBuilder(ITokenWriteOnlyRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public static IntelligentHabitacionUseCaseBuilder Instance()
        {
            var tokenRepository = TokenWriteOnlyRepositoryBuilder.Instance().Build();

            _instance = new IntelligentHabitacionUseCaseBuilder(tokenRepository);
            return _instance;
        }

        public IntelligentHabitacionUseCase Build()
        {
            return new IntelligentHabitacionUseCase(new TokenController(60, "VW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFRlc3QxMjNVbml0VGVzdDEyM1VuaXRUZXN0MTIzVW5pdFQ="), _tokenRepository);
        }
    }
}
