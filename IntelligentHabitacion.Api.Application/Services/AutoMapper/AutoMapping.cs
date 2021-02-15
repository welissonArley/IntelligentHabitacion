using AutoMapper;
using IntelligentHabitacion.Api.Domain.ValueObjects;

namespace IntelligentHabitacion.Api.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entity.User>()
                .ForMember(c => c.ProfileColor, opt => opt.MapFrom(src => new Color().RandomColor()));

            CreateMap<string, Domain.Entity.Phonenumber>()
                .ForMember(c => c.Number, opt => opt.MapFrom(src => src));
        }
    }
}
