using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Specifications;

namespace Restaurant.Persistence.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> query,
            ISpecification<TEntity> spec,
            bool applyPaging = true) where TEntity : class
        {
            if (spec.IgnoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (spec.IsSoftDeleteEnabled && typeof(SoftDeletableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => EF.Property<bool>(x, "IsDeleted") == false);
            }

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (spec.IncludeStrings != null)
            {
                query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            }

            if (spec.IncludeAggregators != null)
            {
                query = spec.IncludeAggregators.Aggregate(query, (current, aggregator) => aggregator(current));
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (applyPaging && spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
