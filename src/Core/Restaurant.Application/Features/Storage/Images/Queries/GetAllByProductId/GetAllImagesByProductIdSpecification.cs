using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId
{
    public class GetAllImagesByProductIdSpecification
        : BaseSpecification<Image>
    {
        public GetAllImagesByProductIdSpecification(GetAllImagesByProductIdQuery query)
        {
            AddIncludeAggregator(x => x.Include(i => i.ProductImage)
                                        .ThenInclude((ProductImage pi) => pi.Product));

            Criteria = i => string.Equals(i.ProductImage.Product.PublicId, query.ProductId);

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = p =>
                    EF.Functions.Like(p.AltText, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case SortField.CreatedAt:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
