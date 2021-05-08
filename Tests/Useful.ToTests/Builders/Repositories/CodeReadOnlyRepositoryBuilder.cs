using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class CodeReadOnlyRepositoryBuilder
    {
        private static CodeReadOnlyRepositoryBuilder _instance;
        private readonly Mock<ICodeReadOnlyRepository> _repository;

        private CodeReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ICodeReadOnlyRepository>();
            }
        }

        public static CodeReadOnlyRepositoryBuilder Instance()
        {
            _instance = new CodeReadOnlyRepositoryBuilder();
            return _instance;
        }

        public CodeReadOnlyRepositoryBuilder GetByUserId(long userId, Code code)
        {
            _repository.Setup(x => x.GetByUserId(userId)).ReturnsAsync(code);
            return this;
        }

        public CodeReadOnlyRepositoryBuilder GetByCode(Code code)
        {
            _repository.Setup(x => x.GetByCode(code.Value)).ReturnsAsync(code);
            return this;
        }

        public ICodeReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
