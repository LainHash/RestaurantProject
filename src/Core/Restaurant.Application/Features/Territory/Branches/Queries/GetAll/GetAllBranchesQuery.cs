using MediatR;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Territory.Branches;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetAll
{
    public record GetAllBranchesQuery()
        : PageQuery, IRequest<PageResult<IEnumerable<BranchResponse>>>
    {
    }
}
