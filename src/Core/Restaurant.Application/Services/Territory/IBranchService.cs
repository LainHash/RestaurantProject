using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Territory.Branches;

namespace Restaurant.Application.Services.Territory
{
    public interface IBranchService
    {
        Task<PageResult<IEnumerable<BranchResponse>>> GetAllAsync(
            GetAllBranchesSpecification specification,
            CancellationToken cancellationToken);
    }
}
