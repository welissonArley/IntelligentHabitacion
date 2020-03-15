using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using Moq;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class LoginFactoryFake : BaseFactoryFake
    {
        public LoginRule GetRule()
        {
            return new LoginRule(GetRepository(), GetCryptographyPassword());
        }

        public IUserRepository GetRepository()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.GetUserByEmail("user1@gmail.com")).Returns(new User
            {
                Name = "User 1",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });

            return repositorioMock.Object;
        }
    }
}
