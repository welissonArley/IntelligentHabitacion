using FluentNHibernate.Mapping;

namespace IntelligentHabitacion.Api.Repository.Token
{
    public class TokenMap : ClassMap<Token>
    {
        public TokenMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Value);
            References(x => x.User).Not.LazyLoad();
        }
    }
}
