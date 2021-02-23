using AutoMapper;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.Linq;

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

            CreateMap<Communication.Request.RequestRegisterHomeJson, Domain.Entity.Home>();

            CreateMap<Communication.Request.RequestUpdateHomeJson, Domain.Entity.Home>()
                .ForMember(c => c.Rooms, opt => opt.MapFrom(w => w.Rooms.Distinct().Where(k => !string.IsNullOrWhiteSpace(k)).Select(k => new Domain.Entity.Room { Name = k })));
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entity.User, Communication.Response.ResponseUserInformationsJson>();
            CreateMap<Domain.Entity.Phonenumber, Communication.Response.ResponsePhonenumberJson>();
            CreateMap<Domain.Entity.EmergencyContact, Communication.Response.ResponseEmergencyContactJson>();
            CreateMap<Domain.Entity.User, Communication.Response.ResponseLoginJson>()
                .ForMember(c => c.IsPartOfOneHome, opt => opt.MapFrom(w => w.HomeAssociationId.HasValue))
                .ForMember(c => c.IsAdministrator, opt => opt.MapFrom(w => w.HomeAssociation != null && w.HomeAssociation.Home.AdministratorId == w.Id));

            CreateMap<Domain.Entity.Home, Communication.Response.ResponseHomeInformationsJson>()
                .ForMember(c => c.NetWork, opt => opt.MapFrom(w => new Communication.Response.ResponseWifiNetworkJson
                { Name = w.NetworksName, Password = w.NetworksPassword }));

            CreateMap<Domain.Entity.Room, Communication.Response.ResponseRoomJson>();
            CreateMap<Domain.Entity.MyFood, Communication.Response.ResponseMyFoodJson>();
        }
    }
}
