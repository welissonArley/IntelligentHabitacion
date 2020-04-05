﻿using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using Moq;
using System;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public abstract class BaseFactoryFake
    {
        protected CryptographyPassword GetCryptographyPassword()
        {
            return new CryptographyPassword("");
        }

        protected IEmailHelper EmailHelperMock()
        {
            var mock = new Mock<IEmailHelper>();
            mock.Setup(c => c.ResetPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            return mock.Object;
        }

        protected ICodeRepository TokenMock()
        {
            var mock = new Mock<ICodeRepository>();
            mock.Setup(c => c.GetByUser(It.IsAny<long>())).Returns(new List<Code>
            {
                new Code()
            });
            mock.Setup(c => c.GetByUserResetPassword(1)).Returns(new Code
            {
                Value = "1234",
                CreateDate = DateTime.UtcNow
            });
            mock.Setup(c => c.GetByUserResetPassword(2)).Returns(new Code
            {
                Value = "1234",
                CreateDate = DateTime.UtcNow.AddHours(-5)
            });

            return mock.Object;
        }

        public ILoggedUser GetLoggedUser()
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup(c => c.User()).Returns(new User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                Email = "user@user.com",
                Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb",
                Name = "User",
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
                        DegreeOfKinship = "Mother",
                        Name = "Contact",
                        Phonenumber = "(31) 9 8888-8888",
                        UserId = 1
                    },
                    new EmergencyContact
                    {
                        DegreeOfKinship = "Mother",
                        Name = "Contact",
                        Phonenumber = "(31) 9 8888-8888",
                        UserId = 1
                    }
                }
            });

            return mock.Object;
        }
    }
}
