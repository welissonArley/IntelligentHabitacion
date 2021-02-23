using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class MyFoodsRepository : IMyFoodsReadOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public MyFoodsRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task<IList<MyFood>> GetByUserId(long userId)
        {
            return await _context.MyFoods
                .AsNoTracking()
                .Where(c => c.UserId == userId && c.Active).ToListAsync();
        }
    }
}
