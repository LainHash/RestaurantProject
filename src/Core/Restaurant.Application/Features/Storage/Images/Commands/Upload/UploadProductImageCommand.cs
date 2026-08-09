using MediatR;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Commands.Upload
{
    public record UploadProductImageCommand(string ProductId,
                                            Stream FileStream,
                                            string FileName,
                                            UploadImageRequest Metadata)
        : IRequest<Result<UploadImageResponse>>
    {
    }
}
