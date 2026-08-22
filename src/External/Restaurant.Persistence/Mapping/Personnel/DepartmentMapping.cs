using AutoMapper;
using Restaurant.Contract.DTOs.Personnel.Departments;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Persistence.DataRecords.Personnel;

namespace Restaurant.Persistence.Mapping.Personnel
{
    internal class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<DepartmentRecord, Department>();

            CreateMap<Department, DepartmentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateDepartmentRequest, Department>();

            CreateMap<UpdateDepartmentRequest, Department>();
        }
    }
}
