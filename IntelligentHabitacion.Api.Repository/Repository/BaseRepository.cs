using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Exception.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public abstract class BaseRepository<TModel> : DbContext, IBaseRepository<TModel> where TModel : ModelBase
    {
        private readonly IDatabaseInformations _databaseInformations;

        protected virtual DbSet<TModel> ModelSet { get; set; }

        protected BaseRepository(IDatabaseInformations databaseInformations)
        {
            _databaseInformations = databaseInformations;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_databaseInformations.DatabaseType())
            {
                case DatabaseInformations.DatabaseType.MySql:
                    {
                        optionsBuilder.UseMySql(_databaseInformations.ConectionString());
                    }
                    break;
                default:
                    {
                        throw new UnknownDatabaseException();
                    }
            }
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
            model.Encrypt();
            ModelSet.Add(model);
            SaveChanges();
        }

        public void Update(TModel model)
        {
            model.Encrypt();
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
