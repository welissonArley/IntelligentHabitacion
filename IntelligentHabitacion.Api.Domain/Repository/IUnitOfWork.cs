using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
