using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.MyFoods
{
    public interface IMyFoodsUpdateOnlyRepository
    {
        Task<Entity.MyFood> GetById_Update(long myFoodId, long userId);
        void Update(Entity.MyFood myFood);
    }
}
