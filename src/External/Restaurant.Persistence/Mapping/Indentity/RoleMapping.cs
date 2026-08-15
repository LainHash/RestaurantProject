using AutoMapper;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.DataRecords.Identity;

namespace Restaurant.Persistence.Mapping.Indentity
{
    internal class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<RoleRecord, Role>();

            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateRoleRequest, Role>();
            CreateMap<UpdateRoleRequest, Role>();
        }
    }
}
