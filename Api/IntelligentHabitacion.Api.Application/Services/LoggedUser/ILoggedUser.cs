using IntelligentHabitacion.Api.Domain.Entity;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<User> User();
    }
}
