using IntelligentHabitacion.Api.Repository.Model;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IMyFoodRepository : IBaseRepository<MyFood>
    {
        IQueryable<MyFood> GetMyFoods(long userId);
        MyFood GetMyFood(long foodId, long userId);
    }
}
