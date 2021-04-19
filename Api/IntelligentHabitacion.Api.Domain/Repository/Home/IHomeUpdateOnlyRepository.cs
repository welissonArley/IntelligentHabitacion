using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Home
{
    public interface IHomeUpdateOnlyRepository
    {
        Task<Entity.Home> GetById_Update(long homeId);
        void Update(Entity.Home home);
    }
}
