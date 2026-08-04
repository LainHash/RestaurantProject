using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Specifications;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Specifications;

namespace Restaurant.Persistence.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly RestaurantDbContext _context;
        protected readonly DbSet<TEntity> Entity;

        public Repository(RestaurantDbContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await Entity.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity, specification);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity, specification);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public void Add(TEntity entity)
        {
            Entity.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Entity.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Entity.Remove(entity);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity.AsQueryable(), specification, applyPaging: false);

            return await query.CountAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity.AsQueryable(), specification, applyPaging: false);

            return await query.AnyAsync(cancellationToken);
        }
    }
}
