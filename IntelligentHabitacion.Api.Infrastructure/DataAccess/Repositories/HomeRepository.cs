using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class HomeRepository : IHomeWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public HomeRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task Add(User administrator, Home home)
        {
            var userToBeAdministrator = await _context.Users
                .Include(c => c.Phonenumbers)
                .Include(c => c.EmergencyContacts)
                .FirstOrDefaultAsync(c => c.Id == administrator.Id && c.Active);

            home.AdministratorId = administrator.Id;
            userToBeAdministrator.HomeAssociation = new HomeAssociation
            {
                JoinedOn = DateTime.UtcNow,
                Home = home
            };

            _context.Users.Update(userToBeAdministrator);
        }
    }
}
