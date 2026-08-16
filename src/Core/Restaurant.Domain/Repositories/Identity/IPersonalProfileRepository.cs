using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IPersonalProfileRepository : IRepository<PersonalProfile>
    {
        Task<PersonalProfile?> FindByUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}
