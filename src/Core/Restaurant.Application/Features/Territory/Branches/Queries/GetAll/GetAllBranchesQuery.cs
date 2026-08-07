using MediatR;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetAll
{
    public record GetAllBranchesQuery()
        : PageQuery, IRequest<PageResult<IEnumerable<BranchResponse>>>
    {
    }
}
