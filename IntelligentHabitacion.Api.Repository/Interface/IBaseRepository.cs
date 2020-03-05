using IntelligentHabitacion.Api.Repository.Model;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Interface
{
    public interface IBaseRepository<TModel> where TModel : ModelBase
    {
        IQueryable<TModel> GetAllActive();
        TModel GetById(long id);
        void Create(TModel entidade);
        void Update(TModel entidade);
        void DeleteOnDataBase(TModel model);
        void DeleteOnSystem(TModel model);
    }
}
