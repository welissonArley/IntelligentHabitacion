using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public abstract class BaseRepository<TModel> : DbContext, IBaseRepository<TModel> where TModel : ModelBase
    {
        protected virtual DbSet<TModel> ModelSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=intelligenthabitacion;Uid=root;Pwd=@Ioasys;");
        }

        public virtual IQueryable<TModel> GetAllActive()
        {
            var models = ModelSet.Where(c => c.Active);
            foreach (var model in models)
                model.Decrypt();

            return models;
        }

        public virtual TModel GetById(long id)
        {
            var model = ModelSet.FirstOrDefault(c => c.Active && c.Id == id);
            model.Decrypt();

            return model;
        }

        public void Create(TModel model)
        {
            model.Active = true;
            model.Encrypty();
            ModelSet.Add(model);
            SaveChanges();
        }

        public void Update(TModel model)
        {
            model.Encrypty();
            SaveChanges();
        }

        public void DeleteOnDatabase(TModel model)
        {
            ModelSet.Remove(model);
            SaveChanges();
        }

        public void DeleteOnSystem(TModel model)
        {
            model.Active = false;
            Update(model);
        }
    }
}
