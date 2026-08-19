using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Personnel.Departments.Queries.GetAll
{
    public class GetAllDepartmentsSpecification
        : BaseSpecification<Department>
    {
        public GetAllDepartmentsSpecification(GetAllDepartmentsQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = d =>
                    EF.Functions.Like(d.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(d.Description, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case SortField.CreatedAt:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(d => d.CreatedAt);
                    else
                        ApplyOrderByDescending(d => d.CreatedAt);
                    break;
                case SortField.Name:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(d => d.Name);
                    else
                        ApplyOrderByDescending(d => d.Name);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
