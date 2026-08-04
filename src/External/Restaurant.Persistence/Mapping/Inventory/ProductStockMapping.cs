using AutoMapper;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Mapping.Inventory
{
    internal class ProductStockMapping : Profile
    {
        public ProductStockMapping()
        {
            CreateMap<ProductStockRecord, ProductStock>();
        }
    }
}
