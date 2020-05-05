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
            return IncludeModel().FirstOrDefault(c => c.UserId == userId && c.Id == foodId);
        }

        public IQueryable<MyFood> GetMyFoods(long userId)
        {
            return IncludeModel().Where(c => c.UserId == userId);
        }
    }
}
