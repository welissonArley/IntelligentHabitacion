using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Token
{
    public interface ITokenReadOnlyRepository
    {
        Task<Entity.Token> GetByUserId(long id);
    }
}
