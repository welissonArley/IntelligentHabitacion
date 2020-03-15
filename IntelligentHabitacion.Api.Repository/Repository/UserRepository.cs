using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User GetUserByEmail(string email)
        {
            var userTemp = new User
            {
                Email = email,
                EmergecyContacts = new List<EmergencyContact>(),
                Phonenumbers = new List<Phonenumber>()
            };
            userTemp.Encripty();

            var response = Session.Query<User>().FirstOrDefault(c => c.Active && c.Email.Equals(userTemp.Email));

            response?.Decrypt();

            return response;
        }
    }
}
