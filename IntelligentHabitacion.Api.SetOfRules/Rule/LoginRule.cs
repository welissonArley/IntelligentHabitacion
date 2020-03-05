using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using System;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        private readonly IUserRepository _userRepository;

        public LoginRule(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void DoLogin()
        {
            _userRepository.Create(new Repository.Model.User
            {
                Active = true,
                CreateDate = DateTime.Today,
                Email = "df",
                Name = "df",
                Password = "df"
            });

            var x = _userRepository.GetAllActive().ToList();
        }
    }
}
