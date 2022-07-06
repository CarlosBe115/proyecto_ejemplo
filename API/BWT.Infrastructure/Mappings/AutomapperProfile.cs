using AutoMapper;
using BWT.Core.DTOs;
using BWT.Core.Entities;

namespace BWT.Infrastructure.Mappings
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Access, AccessDTO>();
            CreateMap<AccessDTO, Access>();

            CreateMap<Clans, ClansDTO>();
            CreateMap<ClansDTO, Clans>();

            CreateMap<Games, GamesDTO>();
            CreateMap<GamesDTO, Games>();

            CreateMap<Partners, PartnersDTO>();
            CreateMap<PartnersDTO, Partners>();

            CreateMap<SocialNetworks, SocialNetworksDTO>();
            CreateMap<SocialNetworksDTO, SocialNetworks>();

            CreateMap<UserClan, UserClanDTO>();
            CreateMap<UserClanDTO, UserClan>();

            CreateMap<UserInfo, UserInfoDTO>();
            CreateMap<UserInfoDTO, UserInfo>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<UCRol, UCRolDTO>();
            CreateMap<UCRolDTO, UCRol>();
        }
    }
}
