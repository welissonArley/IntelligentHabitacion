using IntelligentHabitacion.Api.Repository.DatabaseInformations;
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
        public UserRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }

        private IIncludableQueryable<User, ICollection<EmergencyContact>> IncludeModel()
        {
            return ModelSet.Where(c => c.Active).Include(c => c.Phonenumbers)
                .Include(c => c.HomeAssociation).ThenInclude(c => c.Home)
                .Include(c => c.EmergecyContacts);
        }

        public override IQueryable<User> GetAllActive()
        {
            var models = IncludeModel();
            foreach (var model in models)
                model.Decrypt();

            return models;
        }

        public override User GetById(long id)
        {
            var model = IncludeModel().FirstOrDefault(c => c.Id == id);
            model?.Decrypt();

            return model;
        }

        public User GetByEmail(string email)
        {
            var userTemp = TempUserWithEmail(email);

            var response = IncludeModel().FirstOrDefault(c => c.Active && c.Email.Equals(userTemp.Email));

            response?.Decrypt();

            return response;
        }

        public bool EmailAlreadyBeenRegistered(string email)
        {
            var userTemp = TempUserWithEmail(email);

            return ModelSet.Any(c => c.Active && c.Email.Equals(userTemp.Email));
        }

        private User TempUserWithEmail(string email)
        {
            var userTemp = new User
            {
                Email = email,
                EmergecyContacts = new List<EmergencyContact>(),
                Phonenumbers = new List<Phonenumber>()
            };
            userTemp.Encrypt();

            return userTemp;
        }
    }
}
