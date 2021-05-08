using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Domain.Repository.Token;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.TokenController;

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
            return new IntelligentHabitacionUseCase(TokenControllerBuilder.Instance().Build(), _tokenRepository);
        }
    }
}
