using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser;
using IntelligentHabitacion.Exception;
using System.Linq;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Requests;
using Xunit;

namespace Validators.Test.User.RegisterUser
{
    public class RegisterUserValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_NameEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Name = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_EMPTY));
        }

        [Fact]
        public async Task Validade_EmailEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Email = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_EMPTY));
        }

        [Fact]
        public async Task Validade_PushNotificationIdEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.PushNotificationId = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PUSHNOTIFICATION_INVALID));
        }

        [Fact]
        public async Task Validade_EmailInvalidFormat()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Email = "usertest.com";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_INVALID));
        }

        [Fact]
        public async Task Validade_ExistActiveUserWithEmail()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().ExistActiveUserWithEmail(user.Email).Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }

        [Fact]
        public async Task Validade_PasswordEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Password = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public async Task Validade_PasswordInvalid()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Password = "@";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }

        [Fact]
        public async Task Validade_PhonenumbersEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Phonenumbers.Clear();

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMPTY));
        }

        [Fact]
        public async Task Validade_EmergencyContactEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.Clear();

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }

        [Fact]
        public async Task Validade_EmergencyContact1NameEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Name = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public async Task Validade_EmergencyContact2NameEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.Last().Name = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2NameEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Name = "";
            user.EmergencyContacts.Last().Name = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public async Task Validade_EmergencyContact1RelationshipEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Relationship = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public async Task Validade_EmergencyContact2RelationshipEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.Last().Relationship = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2RelationshipEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Relationship = "";
            user.EmergencyContacts.Last().Relationship = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public async Task Validade_EmergencyContact1PhonenumberEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
        }

        [Fact]
        public async Task Validade_EmergencyContact2PhonenumberEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.Last().Phonenumber = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2PhonenumberEmpty()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = "";
            user.EmergencyContacts.Last().Phonenumber = "";

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }

        [Fact]
        public async Task Validade_MoreThan2PhoneNumbers()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Phonenumbers.Add("+55 37 9 2000-0000");
            user.Phonenumbers.Add("+55 37 9 3000-0000");
            user.Phonenumbers.Add("+55 37 9 4000-0000");

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_MAX_TWO));
        }

        [Fact]
        public async Task Validade_MoreThan2EmergencyContact()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.Add(RequestEmergencyContactBuilder.Instance().Build());
            user.EmergencyContacts.Add(RequestEmergencyContactBuilder.Instance().Build());
            user.EmergencyContacts.Add(RequestEmergencyContactBuilder.Instance().Build());

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_MAX_TWO));
        }

        [Fact]
        public async Task Validade_SamePhoneNumbers()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.Phonenumbers.Clear();
            user.Phonenumbers.Add("+55 37 9 0000-0000");
            user.Phonenumbers.Add("+55 37 9 0000-0000");

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBERS_ARE_SAME));
        }

        [Fact]
        public async Task Validade_EmergencyContactSamePhoneNumbers()
        {
            var user = RequestRegisterUserBuilder.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = user.EmergencyContacts.Last().Phonenumber;

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new RegisterUserValidation(userReadOnlyRepository);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER));
        }
    }
}
