using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Identity
{
    internal class OtpVerificationRepository(RestaurantDbContext context) 
        : Repository<OtpVerification>(context), IOtpVerificationRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<OtpVerification?> FindActiveAsync(int userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
        {
            return await _context.OtpVerifications
                .Where(x => x.UserId == userId &&
                            x.Purpose == purpose &&
                            x.IsAvailable &&
                            x.UsedAt == null &&
                            x.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
