using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using IntelligentHabitacion.Api.SetOfRules.Token.JWT;
using Moq;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class FriendFactoryFake : BaseFactoryFake
    {
        public FriendRule GetRule()
        {
            return new FriendRule(GetLoggedUserWithouHome(), new TokenController(60), new UserFactoryFake().GetRepository(), TokenMock());
        }

        public FriendRule GetRuleLoggedUserAdministrator()
        {
            return new FriendRule(GetLoggedUserAdministrator(), new TokenController(60), GetRepositoryUserAdministrator(), TokenMock());
        }

        public FriendRule GetRuleLoggedUserWithoutFriend()
        {
            return new FriendRule(GetLoggedUserWithoutFriends(), new TokenController(60), GetRepositoryWithoutFriend(), TokenMock());
        }

        public IUserRepository GetRepositoryWithoutFriend()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.GetByHome(1)).Returns(new List<User>());

            return repositorioMock.Object;
        }
        public IUserRepository GetRepositoryUserAdministrator()
        {
            var repositorioMock = new Mock<IUserRepository>();
            repositorioMock.Setup(c => c.GetByHome(1)).Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    HomeAssociation = new HomeAssociation
                    {
                        JoinedOn = DateTime.Today
                    },
                    Phonenumbers = new List<Phonenumber>
                    {
                        new Phonenumber
                        {
                            Number = "(31) 9 9999-9999",
                            UserId = 1
                        },
                        new Phonenumber
                        {
                            Number = "(31) 9 9999-9999",
                            UserId = 1
                        }
                    },
                    EmergecyContacts = new List<EmergencyContact>
                    {
                        new EmergencyContact
                        {
                            Relationship = "Mother",
                            Name = "Contact",
                            Phonenumber = "(31) 9 8888-8888",
                            UserId = 1
                        },
                        new EmergencyContact
                        {
                            Relationship = "Mother",
                            Name = "Contact",
                            Phonenumber = "(31) 9 8888-8888",
                            UserId = 1
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    HomeAssociation = new HomeAssociation
                    {
                        JoinedOn = DateTime.Today
                    },
                    Phonenumbers = new List<Phonenumber>
                    {
                        new Phonenumber
                        {
                            Number = "(31) 9 9999-9999",
                            UserId = 1
                        },
                        new Phonenumber
                        {
                            Number = "(31) 9 9999-9999",
                            UserId = 1
                        }
                    },
                    EmergecyContacts = new List<EmergencyContact>
                    {
                        new EmergencyContact
                        {
                            Relationship = "Mother",
                            Name = "Contact",
                            Phonenumber = "(31) 9 8888-8888",
                            UserId = 1
                        },
                        new EmergencyContact
                        {
                            Relationship = "Mother",
                            Name = "Contact",
                            Phonenumber = "(31) 9 8888-8888",
                            UserId = 1
                        }
                    }
                }
            });

            return repositorioMock.Object;
        }
    }
}
