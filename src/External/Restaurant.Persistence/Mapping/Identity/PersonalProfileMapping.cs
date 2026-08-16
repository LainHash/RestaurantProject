using AutoMapper;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Mapping.Identity
{
    internal class PersonalProfileMapping : Profile
    {
        public PersonalProfileMapping()
        {
            CreateMap<CompleteProfileRequest, PersonalProfile>();
        }
    }
}
