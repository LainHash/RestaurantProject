using AutoMapper;
using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;
using Restaurant.Application.Models.Results;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Repositories.Catalog;

namespace Restaurant.Persistence.Services.Territory
{
    internal class BranchService : IBranchService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<PageResult<IEnumerable<BranchResponse>>> GetAllAsync(GetAllBranchesSpecification specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
