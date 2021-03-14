using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.OneSignalConfig;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.User;
using IntelligentHabitacion.Communication.Request;
using Refit;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.UseCases.User.RegisterUser
{
    public class RegisterUserUseCase : UseCaseBase, IRegisterUserUseCase
    {
        private readonly UserPreferences _userPreferences;
        private readonly IUserRestService _restService;

        public RegisterUserUseCase(UserPreferences userPreferences) : base("User")
        {
            _userPreferences = userPreferences;
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task Execute(RegisterUserModel userInformations)
        {
            var response = await _restService.CreateUser(Mapper(userInformations), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.SaveInitialUserInfos(new Dtos.UserPreferenceDto
            {
                IsAdministrator = false,
                IsPartOfOneHome = false,
                Id = response.Content.Id,
                Token = GetTokenOnHeaderRequest(response.Headers),
                ProfileColor = response.Content.ProfileColor,
                Name = userInformations.Name,
                Password = userInformations.Password,
                Email = userInformations.Email,
                Width = Application.Current.MainPage.Width
            });
        }

        private RequestRegisterUserJson Mapper(RegisterUserModel userInformations)
        {
            var user = new RequestRegisterUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                Password = userInformations.Password,
                PushNotificationId = OneSignalManager.MyOneSignalId
            };

            user.Phonenumbers.Add(userInformations.PhoneNumber1);

            if (!string.IsNullOrWhiteSpace(userInformations.PhoneNumber2))
                user.Phonenumbers.Add(userInformations.PhoneNumber2);

            user.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = userInformations.EmergencyContact1.Name,
                Relationship = userInformations.EmergencyContact1.Relationship,
                Phonenumber = userInformations.EmergencyContact1.PhoneNumber
            });

            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                user.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    Relationship = userInformations.EmergencyContact2.Relationship,
                    Phonenumber = userInformations.EmergencyContact2.PhoneNumber
                });
            }

            return user;
        }
    }
}
