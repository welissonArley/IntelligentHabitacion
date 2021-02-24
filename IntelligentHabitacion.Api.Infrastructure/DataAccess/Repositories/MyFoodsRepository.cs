using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess.Repositories
{
    public class MyFoodsRepository : IMyFoodsReadOnlyRepository, IMyFoodsWriteOnlyRepository
    {
        private readonly IntelligentHabitacionContext _context;

        public MyFoodsRepository(IntelligentHabitacionContext context) => _context = context;

        public async Task<IList<MyFood>> GetByUserId(long userId)
        {
            return await _context.MyFoods
                .AsNoTracking()
                .Where(c => c.UserId == userId && c.Active).ToListAsync();
        }

        public async Task Add(MyFood myFood)
        {
            await _context.MyFoods.AddAsync(myFood);
        }

        public async Task<MyFood> GetById(long myFoodId, long userId)
        {
            return await _context.MyFoods
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Active && c.Id == myFoodId);
        }

        public void Delete(MyFood myFood)
        {
            _context.MyFoods.Remove(myFood);
        }

        public async Task ChangeAmount(long myFoodId, decimal amount)
        {
            var foodModel = await _context.MyFoods.FirstOrDefaultAsync(c => c.Id == myFoodId);
            foodModel.Quantity = amount;

            _context.Update(foodModel);
        }
    }
}
