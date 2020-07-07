using IntelligentHabitacion.Api.Repository.Model;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IMyFoodRepository : IBaseRepository<MyFood>
    {
        List<MyFood> GetMyFoods(long userId);
        MyFood GetMyFood(long foodId, long userId);
    }
}
