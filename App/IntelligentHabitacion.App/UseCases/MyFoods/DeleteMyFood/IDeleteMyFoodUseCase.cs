using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.DeleteMyFood
{
    public interface IDeleteMyFoodUseCase
    {
        Task Execute(string productId);
    }
}
