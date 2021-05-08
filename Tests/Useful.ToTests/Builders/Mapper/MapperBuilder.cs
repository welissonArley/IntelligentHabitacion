using AutoMapper;
using Useful.ToTests.Builders.Hashids;

namespace Useful.ToTests.Builders.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            var hashids = HashidsBuilder.Instance().Build();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new IntelligentHabitacion.Api.Application.Services.AutoMapper.AutoMapping(hashids));
            });
            return mockMapper.CreateMapper();
        }
    }
}
