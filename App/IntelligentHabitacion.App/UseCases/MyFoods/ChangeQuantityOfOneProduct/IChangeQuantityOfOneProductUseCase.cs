using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.ChangeQuantityOfOneProduct
{
    public interface IChangeQuantityOfOneProductUseCase
    {
        Task Execute(string productId, decimal amount);
    }
}
