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
            var models = Session.Query<TModel>().Where(c => c.Active);
            foreach (var model in models)
                model.Decrypt();

            return models;
        }

        public virtual TModel GetById(long id)
        {
            var model = Session.Get<TModel>(id);
            model.Decrypt();
            return model;
        }

        public virtual void Create(TModel model)
        {
            model.Active = true;
            model.Encripty();
            Session.Save(model);
        }

        public virtual void Update(TModel model)
        {
            model.Encripty();
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
