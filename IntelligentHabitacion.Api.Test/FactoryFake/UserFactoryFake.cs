using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class UserFactoryFake : BaseFactoryFake
    {
        public UserRule GetRule()
        {
            return new UserRule(GetRepository(), GetCryptographyPassword(), GetLoggedUserWithouHome());
        }

        public User UserExistFake()
        {
            return new User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                Name = "User 1",
                Email = "user1@user1.com.br",
                Password = "xxxx",
                Phonenumbers = new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Id = 1,
                        Active = true,
                        Number = "(31) 9 9999-9999",
                        CreateDate = DateTime.Today,
                        UserId = 1
                    }
                },
                EmergecyContacts = new List<EmergencyContact>
                {
                    new EmergencyContact
                    {
                        Id = 1,
                        CreateDate = DateTime.Today,
                        Active = true,
                        Name = "Contact 1",
                        DegreeOfKinship = "Relation 1",
                        Phonenumber = "(31) 9 8888-8888"
                    }
                }
            };
        }

        private IQueryable<User> UserList()
        {
            var userList = new List<User> { UserExistFake() };
            return userList.AsQueryable();
        }

        public IUserRepository GetRepository()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.Create(new User()));
            repositorioMock.Setup(c => c.GetAllActive()).Returns(UserList());
            repositorioMock.Setup(c => c.GetByEmail("user1@gmail.com")).Returns(new User
            {
                Name = "User 1",
                Email = "user1@gmail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>(),
                HomeAssociation = new HomeAssociation
                {
                    Home = new Home()
                }
            });
            repositorioMock.Setup(c => c.GetByEmail("user4@gmail.com")).Returns(new User
            {
                Name = "User 1",
                Email = "user1@gmail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>(),
                HomeAssociation = new HomeAssociation
                {
                    Home = new Home()
                }
            });
            repositorioMock.Setup(c => c.GetById(1)).Returns(new User
            {
                Name = "User 1",
                Email = "user1@gmail.com",
                Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>()
            });
            repositorioMock.Setup(c => c.EmailAlreadyBeenRegistered(UserExistFake().Email)).Returns(true);

            return repositorioMock.Object;
        }
    }
}
