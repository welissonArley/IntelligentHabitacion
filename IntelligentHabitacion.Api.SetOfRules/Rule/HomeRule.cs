using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Services.Interface;
using IntelligentHabitacion.Api.SetOfRules.EmailHelper.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class HomeRule : IHomeRule
    {
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodRepository _myFoodRepository;
        private readonly IHomeAssociationRepository _homeAssociationRepository;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ICodeRepository _codeRepository;
        private readonly IEmailHelper _emailHelper;

        public HomeRule(IHomeRepository homeRepository, ILoggedUser loggedUser, IUserRepository userRepository,
            IMyFoodRepository myFoodRepository, IHomeAssociationRepository homeAssociationRepository,
            IPushNotificationService pushNotificationService, ICodeRepository codeRepository, IEmailHelper emailHelper)
        {
            _homeRepository = homeRepository;
            _loggedUser = loggedUser;
            _userRepository = userRepository;
            _myFoodRepository = myFoodRepository;
            _homeAssociationRepository = homeAssociationRepository;
            _pushNotificationService = pushNotificationService;
            _codeRepository = codeRepository;
            _emailHelper = emailHelper;
        }

        public void Delete(RequestAdminActionJson request)
        {
            var loggedUser = _loggedUser.User();

            var userCode = _codeRepository.GetByUserDeleteHome(loggedUser.Id);

            if (userCode == null || !userCode.Value.Equals(request.Code.ToUpper()))
                throw new CodeOrPasswordInvalidException();

            if (userCode.CreateDate.AddMinutes(5) < DateTimeController.DateTimeNow())
                throw new ExpiredCodeException();

            _codeRepository.DeleteOnDatabase(userCode);

            var home = _homeRepository.GetById(loggedUser.HomeAssociation.HomeId);
            var friends = _userRepository.GetByHome(home.Id);
            var pushNotificationIds = friends.Where(c => c.Id != loggedUser.Id).Select(c => c.PushNotificationId).ToList();
            foreach (var friend in friends)
            {
                var homeassociation = _homeAssociationRepository.GetById(friend.HomeAssociationId.Value);
                friend.HomeAssociationId = null;
                _userRepository.Update(friend);
                _homeAssociationRepository.DeleteOnDatabase(homeassociation);
                var foods = _myFoodRepository.GetMyFoods(friend.Id);
                foreach(var food in foods)
                    _myFoodRepository.DeleteOnDatabase(food);
            }
            _homeRepository.DeleteOnDatabase(home);

            var titles = new Dictionary<string, string>
            {
                { "en", "The Administrator deleted the home 🗑" },
                { "pt", "O Administrador excluiu a Home 🗑" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "The home was deleted. We wish good luck ❤️" },
                { "pt", "A Home foi excluida. Desejamos boa sorte ❤️" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.HomeDeleted, "1" } };
            _pushNotificationService.Send(titles, messages, pushNotificationIds, data);
        }

        public ResponseHomeInformationsJson GetInformations()
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeAssociationId == null)
                throw new UserIsNotPartOfAHomeException();

            return new Mapper.Mapper().MapperModelToJson(loggedUser.HomeAssociation.Home);
        }

        public void Register(RequestHomeJson registerHomeJson)
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeAssociationId != null)
                throw new UserIsPartOfAHomeException();

            var homeModel = new Mapper.Mapper().MapperJsonToModel(registerHomeJson);
            homeModel.AdministratorId = loggedUser.Id;

            var validation = new HomeValidator().Validate(homeModel);

            if (validation.IsValid)
            {
                var userToUpdate = _userRepository.GetById(loggedUser.Id);
                var dateTimeNow = DateTimeController.DateTimeNow();
                userToUpdate.HomeAssociation = new HomeAssociation
                {
                    Active = true,
                    CreateDate = dateTimeNow,
                    JoinedOn = dateTimeNow,
                    Home = homeModel
                };
                _userRepository.Update(userToUpdate);
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }

        public void RequestCodeDeleteHome()
        {
            var loggedUser = _loggedUser.User();

            var codeRandom = new CodeGenerator().Random6Chars();

            var userCodes = _codeRepository.GetByUser(loggedUser.Id);
            foreach (var code in userCodes)
                _codeRepository.DeleteOnDatabase(code);

            _codeRepository.Create(new Code
            {
                Active = true,
                Type = CodeType.DeleteHome,
                CreateDate = DateTimeController.DateTimeNow(),
                Value = codeRandom,
                UserId = loggedUser.Id
            });

            _emailHelper.DeleteHome(loggedUser.Email, codeRandom, loggedUser.Name);
        }

        public void Update(RequestHomeJson updateHomeJson)
        {
            var loggedUser = _loggedUser.User();
            var homeModel = _homeRepository.GetById(loggedUser.HomeAssociation.HomeId);

            homeModel.UpdateDate = DateTimeController.DateTimeNow();
            homeModel.Address = updateHomeJson.Address;
            homeModel.AdditionalAddressInfo = updateHomeJson.AdditionalAddressInfo;
            homeModel.Neighborhood = updateHomeJson.Neighborhood;
            homeModel.NetworksName = updateHomeJson.NetworksName;
            homeModel.NetworksPassword = updateHomeJson.NetworksPassword;
            homeModel.Number = updateHomeJson.Number;
            homeModel.ZipCode = updateHomeJson.ZipCode;

            var validation = new HomeValidator().Validate(homeModel);

            if (validation.IsValid)
                _homeRepository.Update(homeModel);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
