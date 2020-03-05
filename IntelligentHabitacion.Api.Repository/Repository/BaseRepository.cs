using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.Repository.WorkUnit;
using NHibernate;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : ModelBase
    {
        protected ISession Session { get { return WorkUnitNHibernate.WorkUnitNHibernateActive.Session; } }

        public IQueryable<TModel> GetAllActive()
        {
            return Session.Query<TModel>().Where(c => c.Active);
        }

        public virtual TModel GetById(long id)
        {
            return Session.Get<TModel>(id);
        }

        public virtual void Create(TModel model)
        {
            model.Active = true;
            Session.Save(model);
        }

        public virtual void Update(TModel model)
        {
            Session.Update(model);
        }

        public virtual void DeleteOnDataBase(TModel model)
        {
            Session.Delete(model);
        }

        public virtual void DeleteOnSystem(TModel model)
        {
            model.Active = false;
            Update(model);
        }
    }
}
