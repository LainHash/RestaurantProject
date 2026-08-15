using AutoMapper;
using Restaurant.Application.Features.Identity.Roles.Queries.GetAll;
using Restaurant.Application.Features.Identity.Roles.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Identity
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(
            GetAllRolesSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var roles = await _roleRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<RoleResponse>>(roles);
            return Result<IEnumerable<RoleResponse>>
                .Succeed(response, Success<Role>.Retrieved);
        }

        public async Task<Result<RoleResponse>> GetByIdAsync(
            GetRoleByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var role = await _roleRepository.FindAsync(specification, cancellationToken);
            if(role is null)
            {
                return Result<RoleResponse>
                    .Fail(Error<Role>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<RoleResponse>(role);
            return Result<RoleResponse>
                .Succeed(response, Success<Role>.Retrieved);
        }
    }
}
