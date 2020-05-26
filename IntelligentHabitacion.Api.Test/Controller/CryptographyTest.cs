using IntelligentHabitacion.Api.Repository.Cryptography;
using IntelligentHabitacion.Api.Repository.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntelligentHabitacion.Api.Test.Controller
{
    public class CryptographyTest
    {
        [Fact]
        public void TestUserCryptography()
        {
            var model = new User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Email = "email@email.com.br",
                Name = "User",
                Password = "Senha123",
                Phonenumbers = new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Id = 1,
                        Active = true,
                        CreateDate = DateTime.Today,
                        UpdateDate = DateTime.Today,
                        Number = "(31) 9 9999-9999"
                    }
                },
                EmergecyContacts = new List<EmergencyContact>
                {
                    new EmergencyContact
                    {
                        Id = 1,
                        Active = true,
                        CreateDate = DateTime.Today,
                        UpdateDate = DateTime.Today,
                        Name = "Contact",
                        Relationship = "Relation",
                        Phonenumber = "(31) 9 9999-9999"
                    }
                }
            };
            Assert.Equal("email@email.com.br", model.Email);
            model.Encrypt();
            Assert.NotEqual("email@email.com.br", model.Email);
            model.Decrypt();
            Assert.Equal("email@email.com.br", model.Email);
        }

        [Fact]
        public void TestPhonenumberCryptography()
        {
            var model = new Phonenumber
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Number = "(31) 9 9999-9999",
                UserId = 1
            };
            Assert.Equal("(31) 9 9999-9999", model.Number);
            model.Encrypt();
            Assert.NotEqual("(31) 9 9999-9999", model.Number);
            model.Decrypt();
            Assert.Equal("(31) 9 9999-9999", model.Number);
            Assert.Equal(1, model.UserId);
        }

        [Fact]
        public void TestEmergencyContactCryptography()
        {
            var model = new EmergencyContact
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Name = "Contact",
                Relationship = "Relation",
                Phonenumber = "(31) 9 9999-9999",
                UserId = 1
            };
            Assert.Equal("Contact", model.Name);
            model.Encrypt();
            Assert.NotEqual("Contact", model.Name);
            model.Decrypt();
            Assert.Equal("Contact", model.Name);
            Assert.Equal(1, model.UserId);
        }

        [Fact]
        public void TestHomeCryptography()
        {
            var model = new Home
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Address = "Address",
                City = "City",
                Complement = "",
                Country = "Country",
                CountryAbbreviation = "A",
                Neighborhood = "Neighborhood",
                NetworksName = "Name",
                NetworksPassword = "Password",
                Number = "1",
                State = "State",
                ZipCode = "Zipcode"
            };
            Assert.Equal("Address", model.Address);
            model.Encrypt();
            Assert.NotEqual("Address", model.Address);
            model.Decrypt();
            Assert.Equal("Address", model.Address);
            model.Decrypt();
            Assert.Equal("Address", model.Address);
        }

        [Fact]
        public void TestHomeAssociationCryptography()
        {
            var model = new HomeAssociation
            {
                Home = new Home
                {
                    Id = 1,
                    Active = true,
                    CreateDate = DateTime.Today,
                    UpdateDate = DateTime.Today,
                    Address = "Address",
                    City = "City",
                    Complement = "",
                    Country = "Country",
                    CountryAbbreviation = "A",
                    Neighborhood = "Neighborhood",
                    NetworksName = "Name",
                    NetworksPassword = "Password",
                    Number = "1",
                    State = "State",
                    ZipCode = "Zipcode"
                }
            };

            Assert.Equal("Address", model.Home.Address);
            model.Encrypt();
            Assert.NotEqual("Address", model.Home.Address);
            model.Decrypt();
            Assert.Equal("Address", model.Home.Address);
        }

        [Fact]
        public void TestWithoutDatas()
        {
            var model = new Phonenumber
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Number = ""
            };
            Assert.Equal("", model.Number);
            model.Encrypt();
            Assert.Equal("", model.Number);
            model.Decrypt();
            Assert.Equal("", model.Number);
        }

        [Fact]
        public void TestDifferentModel()
        {
            var model = new TestClass
            {
                Name = "Name"
            };

            Assert.Throws<System.Security.Cryptography.CryptographicException>(() => model.Encrypt());
        }

        [Fact]
        public void TestCodeCryptography()
        {
            var model = new Code
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Type = CodeType.ResetPassword,
                UserId = 1,
                Value = "1234"
            };
            Assert.Equal("1234", model.Value);
            model.Encrypt();
            Assert.NotEqual("1234", model.Value);
            model.Decrypt();
            Assert.Equal("1234", model.Value);
        }

        [Fact]
        public void TestMyFoodCryptography()
        {
            var model = new MyFood
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Quantity = 5,
                DueDate = DateTime.Today,
                Manufacturer = "Manufacturer",
                Name = "Name",
                Type = Repository.Model.Type.Box,
                UserId = 1
            };
            Assert.Equal("Manufacturer", model.Manufacturer);
            model.Encrypt();
            Assert.NotEqual("Manufacturer", model.Manufacturer);
            model.Decrypt();
            Assert.Equal("Manufacturer", model.Manufacturer);
            Assert.Equal(1, model.UserId);
        }

        [Fact]
        public void TestKeyToken()
        {
            Assert.True(!string.IsNullOrWhiteSpace(KeyModel.GetKey()));
        }
    }

    public class TestClass : ModelBase
    {
        public string Name { get; set; }

        public override void Decrypt()
        {
            var encryptManager = new Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Dencrypt(Name, salt);
        }

        public override void Encrypt()
        {
            var encryptManager = new Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
        }
    }
}
