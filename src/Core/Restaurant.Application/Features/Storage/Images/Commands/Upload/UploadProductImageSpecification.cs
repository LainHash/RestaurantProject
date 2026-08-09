using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Storage.Images.Commands.Upload
{
    public class UploadProductImageSpecification
        : BaseSpecification<Image>
    {
        public UploadProductImageSpecification(UploadProductImageCommand command)
        {
        }
    }
}
