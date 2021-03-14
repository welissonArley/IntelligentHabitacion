using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.User;
using IntelligentHabitacion.App.Useful.Validator;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Refit;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.User.UpdateUserInformations
{
    public class UpdateUserInformationsUseCase : UseCaseBase, IUpdateUserInformationsUseCase
    {
        private readonly UserPreferences _userPreferences;
        private readonly IUserRestService _restService;

        public UpdateUserInformationsUseCase(UserPreferences userPreferences) : base("User")
        {
            _userPreferences = userPreferences;
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task Execute(UserInformationsModel userInformations)
        {
            ValidateName(userInformations.Name);
            ValidateEmail(userInformations.Email);
            ValidateEmergencyContact(userInformations.EmergencyContact1.Name, userInformations.EmergencyContact1.PhoneNumber, userInformations.EmergencyContact1.Relationship);
            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
                ValidateEmergencyContact(userInformations.EmergencyContact2.Name, userInformations.EmergencyContact2.PhoneNumber, userInformations.EmergencyContact2.Relationship);

            if (string.IsNullOrWhiteSpace(userInformations.PhoneNumber1))
                throw new PhoneNumberEmptyException();

            var request = Mapper(userInformations);

            var response = await _restService.UpdateUser(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            await _userPreferences.SaveUserInformations(userInformations.Name, userInformations.Email);
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }
        private void ValidateEmergencyContact(string name, string phoneNumber, string relationship)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();

            if (string.IsNullOrWhiteSpace(relationship))
                throw new RelationshipToEmptyException();

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new PhoneNumberEmptyException();
        }
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();
        }

        private RequestUpdateUserJson Mapper(UserInformationsModel model)
        {
            var request = new RequestUpdateUserJson
            {
                Name = model.Name,
                Email = model.Email
            };

            request.Phonenumbers.Add(model.PhoneNumber1);

            if (!string.IsNullOrWhiteSpace(model.PhoneNumber2))
                request.Phonenumbers.Add(model.PhoneNumber2);

            request.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = model.EmergencyContact1.Name,
                Relationship = model.EmergencyContact1.Relationship,
                Phonenumber = model.EmergencyContact1.PhoneNumber
            });

            if (!string.IsNullOrWhiteSpace(model.EmergencyContact2.Name))
            {
                request.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = model.EmergencyContact2.Name,
                    Relationship = model.EmergencyContact2.Relationship,
                    Phonenumber = model.EmergencyContact2.PhoneNumber
                });
            }

            return request;
        }
    }
}
