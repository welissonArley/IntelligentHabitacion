using FluentNHibernate.Mapping;
using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Map
{
    public class EmergencyContactMap : ClassMap<EmergencyContact>
    {
        public EmergencyContactMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.UpdateDate);
            Map(x => x.Active);
            Map(x => x.Name);
            Map(x => x.DegreeOfKinship);
            HasMany(x => x.Phonenumbers).KeyColumn("EmergencyContact_Id");
        }
    }
}
