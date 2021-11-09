using AutoMapper;
using Faculty.Common.Dto.User;
using Microsoft.AspNetCore.Identity;
using Faculty.AuthenticationServer.Models;

namespace Faculty.AuthenticationServer.Tools
{
    /// <summary>
    /// Mapping source.
    /// </summary>
    public class SourceMappingProfile : Profile
    {
        /// <summary>
        /// Constructor for set up mapping.
        /// </summary>
        public SourceMappingProfile(UserManager<CustomUser> userManager)
        {
            CreateMap<UserDto, CustomUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Login))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(x => x.Birthday.Date));
            CreateMap<CustomUser, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(x => userManager.GetRolesAsync(x).Result))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(x => x.Birthday.Date));
            CreateMap<UserAddDto, CustomUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Login))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(x => x.Birthday.Date));
            CreateMap<CustomUser, UserAddDto>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(x => x.Birthday.Date));
        }
    }
}
