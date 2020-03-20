using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private IIncludableQueryable<User, ICollection<Phonenumber>> IncludeModel()
        {
            return ModelSet.Where(c => c.Active).Include(c => c.Phonenumbers).Include(c => c.EmergecyContacts).ThenInclude(c => c.Phonenumbers);
        }

        public override IQueryable<User> GetAllActive()
        {
            var models = IncludeModel();
            foreach (var model in models)
                model.Decrypt();

            return models;
        }

        public User GetUserByEmail(string email)
        {
            var userTemp = new User
            {
                Email = email,
                EmergecyContacts = new List<EmergencyContact>(),
                Phonenumbers = new List<Phonenumber>()
            };
            userTemp.Encripty();

            var response = IncludeModel().FirstOrDefault(c => c.Active && c.Email.Equals(userTemp.Email));

            response?.Decrypt();

            return response;
        }
    }
}
