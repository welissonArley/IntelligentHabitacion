using AutoMapper;
using HashidsNet;

namespace Useful.ToTests.Builders.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            var hashids = new Hashids("this is my salt", minHashLength: 3);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new IntelligentHabitacion.Api.Application.Services.AutoMapper.AutoMapping(hashids));
            });
            return mockMapper.CreateMapper();
        }
    }
}
