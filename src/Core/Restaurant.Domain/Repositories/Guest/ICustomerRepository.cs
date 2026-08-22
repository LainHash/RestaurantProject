using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Repositories.Guest
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task<Customer?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Customer?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
