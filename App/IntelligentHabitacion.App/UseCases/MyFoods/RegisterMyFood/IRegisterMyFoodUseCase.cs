using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.RegisterMyFood
{
    public interface IRegisterMyFoodUseCase
    {
        Task<FoodModel> Execute(FoodModel model);
    }
}
