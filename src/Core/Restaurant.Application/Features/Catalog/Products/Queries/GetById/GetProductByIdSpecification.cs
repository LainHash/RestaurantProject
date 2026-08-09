using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetById
{
    public class GetProductByIdSpecification
        : BaseSpecification<Product>
    {
        public GetProductByIdSpecification(GetProductByIdQuery query)
        {
            EnableSoftDeleteFilter();
            
            Criteria = p => string.Equals(p.PublicId, query.Id);

            AddInclude(p => p.ProductCategory);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
            AddIncludeAggregator(x => x.Include(p => p.ProductImages)
                                        .ThenInclude((ProductImage pi) => pi.Image));
        }
    }
}
