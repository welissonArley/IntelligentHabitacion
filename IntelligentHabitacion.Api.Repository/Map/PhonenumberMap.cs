using FluentNHibernate.Mapping;
using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Map
{
    public class PhonenumberMap : ClassMap<Phonenumber>
    {
        public PhonenumberMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.UpdateDate);
            Map(x => x.Active);
            Map(x => x.Number);
        }
    }
}
