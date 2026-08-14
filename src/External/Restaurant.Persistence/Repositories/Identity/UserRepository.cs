using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Identity
{
    internal class UserRepository : IUserRepository
    {
        private readonly RestaurantDbContext _context;

        public UserRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser?> GetByPublicIdAsync(string publicId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PublicId == publicId);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = expiryTime;
                user.UpdatedAt = DateTime.UtcNow;
                
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevokeRefreshTokenAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                user.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
