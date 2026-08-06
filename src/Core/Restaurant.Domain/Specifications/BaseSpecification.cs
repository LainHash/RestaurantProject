using System.Linq.Expressions;

namespace Restaurant.Domain.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; }
            = new();

        public List<string> IncludeStrings { get; }
            = new();

        public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> IncludeAggregators { get; }
            = new();

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public bool IgnoreQueryFilters { get; private set; }

        public bool IsSoftDeleteEnabled { get; private set; }

        protected void AddIgnoreQueryFilters()
        {
            IgnoreQueryFilters = true;
        }

        protected void EnableSoftDeleteFilter()
        {
            IsSoftDeleteEnabled = true;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddIncludeAggregator(Func<IQueryable<TEntity>, IQueryable<TEntity>> aggregator)
        {
            IncludeAggregators.Add(aggregator);
        }

        protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        protected void ApplyPaging(int page, int pageSize)
        {
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            IsPagingEnabled = true;
        }
    }
}
