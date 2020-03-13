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
                        DegreeOfKinship = "Relation",
                        Phonenumbers = new List<Phonenumber>
                        {
                            new Phonenumber
                            {
                                Id = 2,
                                Active = true,
                                CreateDate = DateTime.Today,
                                UpdateDate = DateTime.Today,
                                Number = "(31) 9 9999-9999"
                            }
                        }
                    }
                }
            };
            Assert.Equal("email@email.com.br", model.Email);
            model.Encripty();
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
                Number = "(31) 9 9999-9999"
            };
            Assert.Equal("(31) 9 9999-9999", model.Number);
            model.Encripty();
            Assert.NotEqual("(31) 9 9999-9999", model.Number);
            model.Decrypt();
            Assert.Equal("(31) 9 9999-9999", model.Number);
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
                DegreeOfKinship = "Relation",
                Phonenumbers = new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Id = 2,
                        Active = true,
                        CreateDate = DateTime.Today,
                        UpdateDate = DateTime.Today,
                        Number = "(31) 9 9999-9999"
                    }
                }
            };
            Assert.Equal("Contact", model.Name);
            model.Encripty();
            Assert.NotEqual("Contact", model.Name);
            model.Decrypt();
            Assert.Equal("Contact", model.Name);
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
            model.Encripty();
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

            Assert.Throws<System.Security.Cryptography.CryptographicException>(() => model.Encripty());
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

        public override void Encripty()
        {
            var encryptManager = new Cryptography();
            var salt = KeyModel.GetKey(this);

            Name = encryptManager.Encrypt(Name, salt);
        }
    }
}
