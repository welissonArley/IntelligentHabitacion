using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class HomeRepository : BaseRepository<Home>, IHomeRepository
    {
        public HomeRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }

        private IIncludableQueryable<Home, ICollection<User>> IncludeModel()
        {
            return ModelSet.Where(c => c.Active).Include(c => c.Users);
        }

        public override IQueryable<Home> GetAllActive()
        {
            var models = IncludeModel();
            foreach (var model in models)
                model.Decrypt();

            return models;
        }

        public override Home GetById(long id)
        {
            var model = IncludeModel().FirstOrDefault(c => c.Id == id);
            model?.Decrypt();

            return model;
        }
    }
}
