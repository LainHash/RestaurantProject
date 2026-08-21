using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User?> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
