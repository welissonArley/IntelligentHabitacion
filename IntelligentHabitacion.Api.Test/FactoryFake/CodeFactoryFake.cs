using IntelligentHabitacion.Api.Repository.Interface;
using Moq;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class CodeFactoryFake : BaseFactoryFake
    {
        public ICodeRepository GetRepository()
        {
            var repositorioMock = new Mock<ICodeRepository>();

            return repositorioMock.Object;
        }
    }
}
