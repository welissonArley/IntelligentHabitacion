using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public UserRepository(IntelligentHabitacionContext context) => this._context = context;

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }
}
