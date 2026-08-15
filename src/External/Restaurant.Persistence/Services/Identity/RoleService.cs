using AutoMapper;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Repositories.Identity;

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
    }
}
