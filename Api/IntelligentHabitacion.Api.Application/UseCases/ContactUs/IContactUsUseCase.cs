using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ContactUs
{
    public interface IContactUsUseCase
    {
        Task<ResponseOutput> Execute(RequestContactUsJson request);
    }
}
