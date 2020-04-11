using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class HomeRule : IHomeRule
    {
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly ILoggedUser _loggedUser;

        public HomeRule(IHomeRepository homeRepository, ILoggedUser loggedUser, IUserRepository userRepository)
        {
            _homeRepository = homeRepository;
            _loggedUser = loggedUser;
            _userRepository = userRepository;
        }

        public ResponseHomeInformationsJson GetInformations()
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeId == null)
                throw new UserIsNotPartOfAHomeException();

            return new Mapper.Mapper().MapperModelToJson(loggedUser.Home);
        }

        public void Register(RequestRegisterHomeJson registerHomeJson)
        {
            var loggedUser = _loggedUser.User();
            if (loggedUser.HomeId != null)
                throw new UserIsPartOfAHomeException();

            var homeModel = new Mapper.Mapper().MapperJsonToModel(registerHomeJson);
            homeModel.AdministratorId = loggedUser.Id;

            var validation = new HomeValidator().Validate(homeModel);

            if (validation.IsValid)
            {
                _homeRepository.Create(homeModel);
                var userToUpdate = _userRepository.GetById(loggedUser.Id);
                userToUpdate.HomeId = homeModel.Id;
                _userRepository.Update(userToUpdate);
            }
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }

        public void Update(RequestRegisterHomeJson updateHomeJson)
        {
            var loggedUser = _loggedUser.User();
            var homeModel = _homeRepository.GetById((long)loggedUser.HomeId);

            homeModel.UpdateDate = DateTimeController.DateTimeNow();
            homeModel.Address = updateHomeJson.Address;
            homeModel.City = updateHomeJson.City.Name;
            homeModel.State = updateHomeJson.City.State.Name;
            homeModel.Country = updateHomeJson.City.State.Country.Name;
            homeModel.CountryAbbreviation = updateHomeJson.City.State.Country.Abbreviation;
            homeModel.Complement = updateHomeJson.Complement;
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
