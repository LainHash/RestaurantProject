using AutoMapper;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Persistence.Mapping.Identity
{
    internal class PersonalProfileMapping : Profile
    {
        public PersonalProfileMapping()
        {
            CreateMap<CompleteProfileRequest, PersonalProfile>();

            CreateMap<PersonalProfile, PersonalProfileResponse>();
        }
    }
}
