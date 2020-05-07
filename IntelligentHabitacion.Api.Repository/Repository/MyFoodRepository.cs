using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using System.Linq;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class MyFoodRepository : BaseRepository<MyFood>, IMyFoodRepository
    {
        public MyFoodRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }

        private IQueryable<MyFood> IncludeModel()
        {
            return ModelSet.Where(c => c.Active);
        }

        public MyFood GetMyFood(long foodId, long userId)
        {
            var model = IncludeModel().FirstOrDefault(c => c.UserId == userId && c.Id == foodId);
            model?.Decrypt();

            return model;
        }

        public IQueryable<MyFood> GetMyFoods(long userId)
        {
            var models = IncludeModel().Where(c => c.UserId == userId);
            foreach (var model in models)
                model.Decrypt();
            return models;
        }
    }
}
