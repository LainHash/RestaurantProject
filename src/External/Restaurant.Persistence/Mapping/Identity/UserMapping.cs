using AutoMapper;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Mapping.Identity
{
    internal class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<RegisterRequest, User>();

            CreateMap<User, AccountResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
