using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
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
            return new UserRule(GetRepository(), GetCryptographyPassword(), null);
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
                        CreateDate = DateTime.Today
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
                        Phonenumbers = new List<Phonenumber>
                        {
                            new Phonenumber
                            {
                                Id = 2,
                                Active = true,
                                Number = "(31) 9 9999-9999",
                                CreateDate = DateTime.Today
                            }
                        }
                    }
                }
            };
        }

        private IQueryable<User> UserList()
        {
            var userList = new List<User> { UserExistFake() };
            return userList.AsQueryable();
        }

        public ILoggedUser GetLoggedUser()
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup(c => c.User()).Returns(new User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                Email = "user@user.com"
            });

            return mock.Object;
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
                EmergecyContacts = new List<EmergencyContact>()
            });

            return repositorioMock.Object;
        }
    }
}
