using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public UserRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(c => c.Email.Equals(email) && c.Active);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .Include(c => c.Phonenumbers)
                .Include(c => c.EmergencyContacts)
                .Include(c => c.HomeAssociation).ThenInclude(c => c.Home).ThenInclude(c => c.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Active);
        }

        public async Task<User> GetByEmailPassword(string email, string password)
        {
            return await _context.Users
                .Include(c => c.HomeAssociation).ThenInclude(c => c.Home)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password) && c.Active);
        }

        public async Task<User> GetByEmail_Update(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Active);
        }

        public async Task<User> GetById_Update(long id)
        {
            return await _context.Users
                .Include(c => c.Phonenumbers)
                .Include(c => c.EmergencyContacts)
                .FirstOrDefaultAsync(c => c.Id == id && c.Active);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
