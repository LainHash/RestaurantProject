using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAll
{
    public class GetAllImagesSpecification
        : BaseSpecification<Image>
    {
        public GetAllImagesSpecification(GetAllImagesQuery query)
        {
            AddInclude(x => x.ProductImage);

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
