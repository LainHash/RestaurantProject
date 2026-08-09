using MediatR;
using Restaurant.Application.Services.Storage;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId
{
    internal class GetAllImagesByProductIdQueryHandler(IImageService imageService)
        : IRequestHandler<GetAllImagesByProductIdQuery, PageResult<IEnumerable<ImageResponse>>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<PageResult<IEnumerable<ImageResponse>>> Handle(GetAllImagesByProductIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllImagesByProductIdSpecification(request);
            var response = await _imageService.GetAllByProductIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
