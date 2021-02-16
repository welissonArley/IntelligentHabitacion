using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public UserRepository(IntelligentHabitacionContext context) => _context = context;

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public bool ExistActiveUserWithEmail(string email)
        {
            return _context.Users.Any(c => c.Email.Equals(email) && c.Active);
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .Include(c => c.Phonenumbers)
                .Include(c => c.EmergencyContacts)
                .AsNoTracking()
                .FirstOrDefault(c => c.Email.Equals(email));
        }
    }
}
