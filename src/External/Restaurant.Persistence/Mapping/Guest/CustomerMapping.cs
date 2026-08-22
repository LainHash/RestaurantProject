using AutoMapper;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Persistence.Mapping.Guest
{
    internal class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.PersonalProfile, opt => opt.MapFrom(src => src.User.PersonalProfile));
        }
    }
}
