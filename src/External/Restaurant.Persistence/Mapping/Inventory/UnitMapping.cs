using AutoMapper;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Enums;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Mapping.Inventory
{
    internal class UnitMapping : Profile
    {
        public UnitMapping()
        {
            CreateMap<UnitRecord, Unit>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<UnitType>(src.Type)));
        }
    }
}
