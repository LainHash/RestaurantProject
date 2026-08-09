using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Repositories.Storage;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Storage
{
    internal class ProductImageRepository(RestaurantDbContext context)
        : Repository<ProductImage>(context), IProductImageRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<int> CountByProductIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.ProductImages
                .CountAsync(pi => pi.ProductId == productId, cancellationToken);
        }

        public async Task UnsetPrimaryAsync(int productId, CancellationToken cancellationToken = default)
        {
            // Lấy tất cả Image đang là primary của product này
            var primaryImages = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .Include(pi => pi.Image)
                .Where(pi => pi.IsPrimary)
                .Select(pi => pi.Image)
                .ToListAsync(cancellationToken);

            foreach (var image in primaryImages)
            {
                image.ProductImage.RemovePrimary();
            }
        }
    }
}
