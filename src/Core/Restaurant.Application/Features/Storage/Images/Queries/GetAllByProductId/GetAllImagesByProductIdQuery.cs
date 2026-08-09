using MediatR;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId
{
    public record GetAllImagesByProductIdQuery(string ProductId)
        : PageQuery, IRequest<PageResult<IEnumerable<ImageResponse>>>
    {
    }
}
