using AutoMapper;
using Restaurant.Contract.DTOs.Territory.Branches;
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
                .ForMember(dest => dest.OpenTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.OpenTime)))
                .ForMember(dest => dest.CloseTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.CloseTime)));

            CreateMap<Branch, BranchResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));
        }
    }
}
