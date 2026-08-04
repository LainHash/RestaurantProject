using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryRecord, Category>();
        }
    }
}
