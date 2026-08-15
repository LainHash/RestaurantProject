using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
