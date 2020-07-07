using IntelligentHabitacion.Api.Repository.DatabaseInformations;
using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Repository
{
    public class HomeAssociationRepository : BaseRepository<HomeAssociation>, IHomeAssociationRepository
    {
        public HomeAssociationRepository(IDatabaseInformations databaseInformations) : base(databaseInformations)
        {
        }
    }
}
