using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.ContactUs
{
    public interface IContactUsUseCase
    {
        Task Execute(string message);
    }
}
