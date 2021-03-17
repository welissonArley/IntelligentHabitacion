using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.UpdateMyFood
{
    public interface IUpdateMyFoodUseCase
    {
        Task Execute(FoodModel model);
    }
}
