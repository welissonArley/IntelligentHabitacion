using IntelligentHabitacion.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.GetMyFoods
{
    public interface IGetMyFoodsUseCase
    {
        Task<IList<FoodModel>> Execute();
    }
}
