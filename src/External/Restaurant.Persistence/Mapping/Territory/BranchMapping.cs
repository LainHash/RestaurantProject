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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BranchStatus>(src.Status)));
        }
    }
}
