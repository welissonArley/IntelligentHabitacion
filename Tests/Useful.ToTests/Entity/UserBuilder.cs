using IntelligentHabitacion.Api.Domain.Entity;
using System;
using System.Collections.Generic;
using Useful.ToTests.Builders.Encripter;

namespace Useful.ToTests.Entity
{
    public class UserBuilder
    {
        private static UserBuilder _instance;

        public static UserBuilder Instance()
        {
            _instance = new UserBuilder();
            return _instance;
        }

        public User User_WithoutHomeAssociation()
        {
            return new User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.UtcNow.AddDays(-1),
                Email = "user1@email.com",
                Name = "User 1",
                Password = PasswordEncripterBuilder.Instance().Build().Encrypt("@Password123"),
                ProfileColor = "#000000",
                PushNotificationId = "PushId",
                HomeAssociationId = null,
                Phonenumbers = new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Id = 1,
                        Active = true,
                        CreateDate = DateTime.UtcNow.AddDays(-1),
                        Number = "+55 9 9999-9999",
                        UserId = 1
                    }
                },
                EmergencyContacts = new List<EmergencyContact>
                {
                    new EmergencyContact
                    {
                        Id = 1,
                        Active = true,
                        CreateDate = DateTime.UtcNow.AddDays(-1),
                        Name = "Contact 1",
                        Relationship = "Mother",
                        Phonenumber = "+55 9 8888-8888",
                        UserId = 1
                    }
                }
            };
        }
        public User User_WithHomeAssociation()
        {
            var user = User_WithoutHomeAssociation();
            user.HomeAssociationId = 1;
            user.HomeAssociation = new HomeAssociation
            {
                HomeId = 1,
                Home = new Home
                {
                    Id = 1,
                    Active = true,
                    City = "City",
                    AdditionalAddressInfo = "Additional",
                    Address = "Address",
                    Neighborhood = "Neighborhood",
                    Number = "1",
                    StateProvince = "State",
                    ZipCode = "31.000-000",
                    Country = IntelligentHabitacion.Api.Domain.ValueObjects.CountryEnum.BRAZIL,
                    DeadlinePaymentRent = 15,
                    CreateDate = DateTime.UtcNow
                }
            };

            return user;
        }
    }
}
