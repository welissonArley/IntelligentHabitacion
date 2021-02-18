using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Token
{
    public interface ITokenWriteOnlyRepository
    {
        Task Add(Entity.Token token);
    }
}
