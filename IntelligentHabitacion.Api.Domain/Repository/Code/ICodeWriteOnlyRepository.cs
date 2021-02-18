using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Code
{
    public interface ICodeWriteOnlyRepository
    {
        Task Add(Entity.Code code);
        void DeleteAllFromTheUser(long userId);
    }
}
