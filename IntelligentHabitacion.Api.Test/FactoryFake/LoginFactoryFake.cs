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
            return new LoginRule(GetRepository(), GetCryptographyPassword(), TokenMock(), EmailHelperMock());
        }

        public IUserRepository GetRepository()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.GetByEmail("user1@gmail.com")).Returns(new User
            {
                Id = 1,
                Name = "User 1",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });
            repositorioMock.Setup(c => c.GetByEmail("user2@gmail.com")).Returns(new User
            {
                Id = 0,
                Name = "User 2",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });
            repositorioMock.Setup(c => c.GetByEmail("user3@gmail.com")).Returns(new User
            {
                Id = 2,
                Name = "User 3",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });

            return repositorioMock.Object;
        }
    }
}
