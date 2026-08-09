using Restaurant.Application.Features.Storage.Images.Queries.GetAll;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Storage
{
    public interface IImageService
    {
        Task<PageResult<IEnumerable<ImageResponse>>> GetAllAsync(
            GetAllImagesSpecification specification,
            CancellationToken cancellationToken);
    }
}
