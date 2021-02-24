using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.MyFoods
{
    public interface IMyFoodsReadOnlyRepository
    {
        Task<IList<Entity.MyFood>> GetByUserId(long userId);
        Task<Entity.MyFood> GetById(long myFoodId, long userId);
    }
}
