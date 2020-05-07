using IntelligentHabitacion.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IMyFoodsRule
    {
        Task<string> AddItem(FoodModel model);
        Task EditItem(FoodModel model);
        Task<List<FoodModel>> GetMyFoods();
        Task DeleteMyFood(string id);
        Task ChangeQuantity(FoodModel model);
    }
}
