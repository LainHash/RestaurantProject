using Restaurant.Domain.Specifications;

namespace Restaurant.Domain.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> ToListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
