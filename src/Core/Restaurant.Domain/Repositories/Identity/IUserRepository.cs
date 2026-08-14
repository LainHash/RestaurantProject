using Restaurant.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(int id);
        
        Task<ApplicationUser?> GetByPublicIdAsync(string publicId);

        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task<bool> ExistsByEmailAsync(string email);

        Task UpdateRefreshTokenAsync(int userId, string refreshToken, System.DateTime expiryTime);

        Task RevokeRefreshTokenAsync(int userId);
    }
}
