using System.Linq.Expressions;

namespace Restaurant.Domain.Specifications
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> IncludeAggregators { get; }

        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }

        public bool IgnoreQueryFilters { get; }
        public bool IsSoftDeleteEnabled { get; }
    }
}
