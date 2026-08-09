using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Storage
{
    public interface ICloudinaryService
    {
        Task<CloudinaryUploadResult> UploadAsync(
            Stream fileStream,
            string fileName,
            string? folder = null,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            string publicId,
            CancellationToken cancellationToken = default);
    }
}
