using AutoMapper;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Persistence.DataRecords.Territory;

namespace Restaurant.Persistence.Mapping.Territory
{
    internal class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<BranchRecord, Branch>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BranchStatus>(src.Status)))
                .ForMember(dest => dest.OpenTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.OpenTime)))
                .ForMember(dest => dest.CloseTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.CloseTime)));
        }
    }
}
