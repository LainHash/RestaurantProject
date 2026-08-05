using AutoMapper;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Mapping.Inventory
{
    internal class ProductStockMapping : Profile
    {
        public ProductStockMapping()
        {
            CreateMap<ProductStockRecord, ProductStock>();

            CreateMap<ProductStock, ProductStockResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.Branch.Code))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
