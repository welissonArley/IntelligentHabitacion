using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.JWT;
using IntelligentHabitacion.Api.SetOfRules.Rule;
using Moq;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class FriendFactoryFake : BaseFactoryFake
    {
        public FriendRule GetRule()
        {
            return new FriendRule(GetLoggedUserWithouHome(), new UserFactoryFake().GetRepository(), PushNotification(), TokenMock(), EmailHelperMock(), GetCryptographyPassword());
        }

        public FriendRule GetRuleLoggedUserAdministrator()
        {
            return new FriendRule(GetLoggedUserAdministrator(), GetRepositoryUserAdministrator(), PushNotification(), TokenMock(), EmailHelperMock(), GetCryptographyPassword());
        }

        public FriendRule GetRuleLoggedUserAdministratorTokenExpired()
        {
            var mock = new Mock<ICodeRepository>();
            mock.Setup(c => c.GetByUserChangeAdministrator(1)).Returns(new Code
            {
                Value = "1234",
                CreateDate = DateTime.UtcNow.AddHours(-8)
            });
            mock.Setup(c => c.GetByUserRemoveFriend(1)).Returns(new Code
            {
                Value = "1234",
                CreateDate = DateTime.UtcNow.AddHours(-8)
            });

            return new FriendRule(GetLoggedUserAdministrator(), GetRepositoryUserAdministrator(), PushNotification(), mock.Object, EmailHelperMock(), GetCryptographyPassword());
        }

        public FriendRule GetRuleLoggedUserWithoutFriend()
        {
            return new FriendRule(GetLoggedUserWithoutFriends(), GetRepositoryWithoutFriend(), PushNotification(), TokenMock(), EmailHelperMock(), GetCryptographyPassword());
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
            repositorioMock.Setup(c => c.GetById(2)).Returns(new User
            {
                Name = "User 1",
                Email = "user1@gmail.com",
                Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb",
                Phonenumbers = new List<Phonenumber>(),
                EmergecyContacts = new List<EmergencyContact>(),
                HomeAssociation = new HomeAssociation
                {
                    Home = new Home
                    {
                        AdministratorId = 1
                    },
                    HomeId = 1
                }
            });

            return repositorioMock.Object;
        }
    }
}
