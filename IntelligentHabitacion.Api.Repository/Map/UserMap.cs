using FluentNHibernate.Mapping;
using IntelligentHabitacion.Api.Repository.Model;

namespace IntelligentHabitacion.Api.Repository.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CreateDate);
            Map(x => x.UpdateDate);
            Map(x => x.Active);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Password);
            HasMany(x => x.EmergecyContacts).KeyColumn("User_Id").Cascade.All();
            HasMany(x => x.Phonenumbers).KeyColumn("User_Id").Cascade.All();
        }
    }
}
