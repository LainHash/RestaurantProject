using AutoMapper;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.DataRecords.Identity;

namespace Restaurant.Persistence.Mapping.Indentity
{
    internal class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<RoleRecord, Role>();
        }
    }
}
