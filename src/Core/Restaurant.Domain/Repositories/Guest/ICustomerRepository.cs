using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Repositories.Guest
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> FindByUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}
