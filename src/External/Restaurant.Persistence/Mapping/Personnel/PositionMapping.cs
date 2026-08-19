using AutoMapper;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Persistence.DataRecords.Personnel;

namespace Restaurant.Persistence.Mapping.Personnel
{
    internal class PositionMapping : Profile
    {
        public PositionMapping()
        {
            CreateMap<PositionRecord, Position>();
        }
    }
}
