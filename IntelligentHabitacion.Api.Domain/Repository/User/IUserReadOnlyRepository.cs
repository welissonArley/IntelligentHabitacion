using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistActiveUserWithEmail(string email);
        Task<Entity.User> GetByEmail(string email);
        Task<Entity.User> GetByEmailPassword(string email, string password);
    }
}
