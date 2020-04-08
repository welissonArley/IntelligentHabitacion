using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
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
    }
}
