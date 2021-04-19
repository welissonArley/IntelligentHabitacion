using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Home
{
    public interface IHomeWriteOnlyRepository
    {
        Task Add(Entity.User administrator, Entity.Home home);
    }
}
