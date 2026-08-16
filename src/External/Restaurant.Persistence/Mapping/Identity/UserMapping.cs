using AutoMapper;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Mapping.Identity
{
    internal class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<RegisterRequest, User>();
        }
    }
}
