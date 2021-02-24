using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeQuantityOfOneProduct
{
    public interface IChangeQuantityOfOneProductUseCase
    {
        Task<ResponseOutput> Execute(long id, decimal amount);
    }
}
