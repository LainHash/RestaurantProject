using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IOtpVerificationRepository : IRepository<OtpVerification>
    {
        Task<OtpVerification?> FindActiveAsync(int userId, OtpPurpose purpose, CancellationToken cancellationToken = default);
    }
}
