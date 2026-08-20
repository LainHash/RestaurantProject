using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsSpecification
        : BaseSpecification<Product>
    {
        public GetAllProductsSpecification(GetAllProductsQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(p => p.ProductCategory);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
            AddIncludeAggregator(x => x.Include(p => p.ProductImages)
                                        .ThenInclude((ProductImage pi) => pi.Image));


            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%"));
            }

            if (!string.IsNullOrWhiteSpace(query.CategoryId))
            {
                AddCriteria(p =>
                    p.ProductCategory.PublicId == query.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(query.BrandId))
            {
                AddCriteria(p =>
                    p.Brand!.PublicId == query.BrandId);
            }

            switch (query.SortField)
            {
                case SortField.CreatedAt:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
                case SortField.Name:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
                    break;
                case SortField.Price:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.ProductPrice!.UnitPrice);
                    else
                        ApplyOrderByDescending(p => p.ProductPrice!.UnitPrice);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
