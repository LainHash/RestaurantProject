using MediatR;
using Restaurant.Application.Services.Storage;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAll
{
    internal class GetAllImagesQueryHandler(IImageService imageService)
        : IRequestHandler<GetAllImagesQuery, PageResult<IEnumerable<ImageResponse>>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<PageResult<IEnumerable<ImageResponse>>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllImagesSpecification(request);
            var response = await _imageService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
