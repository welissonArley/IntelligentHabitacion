using AutoMapper;
using IntelligentHabitacion.Api.Domain.ValueObjects;

namespace IntelligentHabitacion.Api.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entity.User>()
                .ForMember(c => c.ProfileColor, opt => opt.MapFrom(src => new Color().RandomColor()));

            CreateMap<string, Domain.Entity.Phonenumber>()
                .ForMember(c => c.Number, opt => opt.MapFrom(src => src));

            CreateMap<Communication.Request.RequestEmergencyContactJson, Domain.Entity.EmergencyContact>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entity.User, Communication.Response.ResponseUserInformationsJson>();
            CreateMap<Domain.Entity.Phonenumber, Communication.Response.ResponsePhonenumberJson>();
            CreateMap<Domain.Entity.EmergencyContact, Communication.Response.ResponseEmergencyContactJson>();
            CreateMap<Domain.Entity.User, Communication.Response.ResponseLoginJson>()
                .ForMember(c => c.IsPartOfOneHome, opt => opt.MapFrom(w => w.HomeAssociationId.HasValue))
                .ForMember(c => c.IsAdministrator, opt => opt.MapFrom(w => w.HomeAssociation != null && w.HomeAssociation.Home.AdministratorId == w.Id));
        }
    }
}
