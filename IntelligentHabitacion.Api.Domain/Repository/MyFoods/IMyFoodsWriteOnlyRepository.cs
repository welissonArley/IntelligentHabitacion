using IntelligentHabitacion.Api.Domain.Entity;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.MyFoods
{
    public interface IMyFoodsWriteOnlyRepository
    {
        Task Add(MyFood myFood);
        void Delete(MyFood myFood);
        Task ChangeAmount(long myFoodId, decimal amount);
    }
}
