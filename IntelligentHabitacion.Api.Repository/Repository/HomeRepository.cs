using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class HomeRepository : BaseRepository<Home>, IHomeRepository
    {
        public HomeRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }
    }
}
