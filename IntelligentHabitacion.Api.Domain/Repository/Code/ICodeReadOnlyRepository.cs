using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Domain.Repository.Code
{
    public interface ICodeReadOnlyRepository
    {
        Task<Entity.Code> GetByUserId(long userId);
        Task<Entity.Code> GetByCode(string code);
    }
}
