using AutoMapper;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Persistence.DataRecords.Pricing;

namespace Restaurant.Persistence.Mapping.Pricing
{
    internal class ProductPriceMapping : Profile
    {
        public ProductPriceMapping()
        {
            CreateMap<ProductPriceRecord, ProductPrice>();
        }
    }
}
