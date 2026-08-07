using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Branches.Queries.GetAll
{
    internal class GetAllBranchesQueryHandler(IBranchService branchService)
                : IRequestHandler<GetAllBranchesQuery, PageResult<IEnumerable<BranchResponse>>>
    {
        private readonly IBranchService _branchService = branchService;

        public async Task<PageResult<IEnumerable<BranchResponse>>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllBranchesSpecification(request);
            var response = await _branchService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
