using MediatR;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Queries.GetAll
{
    public record GetAllImagesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<ImageResponse>>>
    {
    }
}
