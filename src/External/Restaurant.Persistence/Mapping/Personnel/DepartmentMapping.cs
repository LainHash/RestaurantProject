using AutoMapper;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Persistence.DataRecords.Personnel;

namespace Restaurant.Persistence.Mapping.Personnel
{
    internal class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<DepartmentRecord, Department>();
        }
    }
}
