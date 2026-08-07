using AutoMapper;
using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Territory;

namespace Restaurant.Persistence.Services.Territory
{
    internal class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRespository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBranchRepository branchRespository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _branchRespository = branchRespository;
        }

        public async Task<PageResult<IEnumerable<BranchResponse>>> GetAllAsync(
            GetAllBranchesSpecification specification,
            CancellationToken cancellationToken)
        {
            var totalItem = await _branchRespository.CountAsync(specification, cancellationToken);

            var branches = await _branchRespository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<BranchResponse>>(branches);
            return PageResult<IEnumerable<BranchResponse>>
                .Succeed(response, Success<Branch>.Retrieved, totalItem, specification.Skip, specification.Take);
        }
    }
}
