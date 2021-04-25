using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System;
using System.Collections.Generic;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace Validators.Test.User.RegisterUser
{
    public class RegisterUserValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_NameEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_EMPTY));
        }

        [Fact]
        public void Validade_EmailEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_EMPTY));
        }

        [Fact]
        public void Validade_PushNotificationIdEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PUSHNOTIFICATION_INVALID));
        }

        [Fact]
        public void Validade_EmailInvalidFormat()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "usertest.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_INVALID));
        }

        [Fact]
        public void Validade_ExistActiveUserWithEmail()
        {
            var email = "user@test.com";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().ExistActiveUserWithEmail(email).Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = email,
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }

        [Fact]
        public void Validade_PasswordEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Validade_PasswordInvalid()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }

        [Fact]
        public void Validade_PhonenumbersEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMPTY));
        }

        [Fact]
        public void Validade_EmergencyContactEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }

        [Fact]
        public void Validade_EmergencyContact1NameEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2NameEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Phonenumber = "+55 37 9 0000-0002",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2NameEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Phonenumber = "+55 37 9 0000-0002",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1RelationshipEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0001"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2RelationshipEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0002"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2RelationshipEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0001"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0002"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1PhonenumberEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2PhonenumberEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2PhonenumberEmpty()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                Email = "user@test.com",
                Password = "@Password123",
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Relationship = "Sister"
                    }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }

        [Fact]
        public void Validade_MoreThan2PhoneNumbers()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000", "+55 37 9 2000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_MAX_TWO));
        }

        [Fact]
        public void Validade_MoreThan2EmergencyContact()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 3",
                        Phonenumber = "+55 37 9 0000-0003",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_MAX_TWO));
        }

        [Fact]
        public void Validade_SamePhoneNumbers()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 0000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0001",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBERS_ARE_SAME));
        }

        [Fact]
        public void Validade_EmergencyContactSamePhoneNumbers()
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = validator.Validate(new RequestRegisterUserJson
            {
                Name = "User",
                PushNotificationId = Guid.NewGuid().ToString(),
                Phonenumbers = new List<string> { "+55 37 9 0000-0000", "+55 37 9 1000-0000" },
                EmergencyContacts = new List<RequestEmergencyContactJson>
                {
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 1",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Mother"
                    },
                    new RequestEmergencyContactJson
                    {
                        Name = "Contact 2",
                        Phonenumber = "+55 37 9 0000-0000",
                        Relationship = "Sister"
                    }
                },
                Email = "user@test.com",
                Password = "@Password123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER));
        }
    }
}
