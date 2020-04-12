using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using Moq;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class HomeFactoryFake : BaseFactoryFake
    {
        public HomeRule GetRule()
        {
            return new HomeRule(GetRepository(), GetLoggedUserWithouHome(), new UserFactoryFake().GetRepository());
        }

        public HomeRule GetRuleLoggedUserAdministrator()
        {
            return new HomeRule(GetRepository(), GetLoggedUserAdministrator(), new UserFactoryFake().GetRepository());
        }

        public IHomeRepository GetRepository()
        {
            var repositorioMock = new Mock<IHomeRepository>();
            repositorioMock.Setup(c => c.Create(new Home()));
            repositorioMock.Setup(c => c.GetById(1)).Returns(new Home());

            return repositorioMock.Object;
        }
    }
}
