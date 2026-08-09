using MediatR;
using Restaurant.Application.Services.Storage;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Commands.Upload
{
    internal class UploadProductImageCommandHandler(IImageService imageService)
        : IRequestHandler<UploadProductImageCommand, Result<UploadImageResponse>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<Result<UploadImageResponse>> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var specification = new UploadProductImageSpecification(request);
            var response = await _imageService.UploadProductImageAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
