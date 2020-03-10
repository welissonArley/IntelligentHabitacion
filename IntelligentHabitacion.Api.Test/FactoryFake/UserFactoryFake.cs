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
            return new UserRule(GetRepository(), GetCryptographyPassword());
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

        public IUserRepository GetRepository()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.Create(new User()));
            repositorioMock.Setup(c => c.GetAllActive()).Returns(UserList());

            return repositorioMock.Object;
        }
    }
}
