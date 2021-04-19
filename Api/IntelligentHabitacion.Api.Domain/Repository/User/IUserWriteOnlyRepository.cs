using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.User
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(Entity.User user);
    }
}
