using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Identity
{
    internal class OtpVerificationRepository(RestaurantDbContext context) 
        : Repository<OtpVerification>(context), IOtpVerificationRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
