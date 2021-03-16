using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.Home
{
    [Headers("Content-Type: application/json")]
    public interface IZipCodeService
    {
        [Get("/{zipcode}/json")]
        Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode);
    }
}
