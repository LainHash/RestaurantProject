using AutoMapper;
using ConvenienceStore.Contract.DTOs.Authentication;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Email;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Auth
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider,
            IMapper mapper,
            ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<AuthenticationResponse>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Result<AuthenticationResponse>
                    .Fail("Incorrect email or password.", HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                return Result<AuthenticationResponse>
                    .Fail("Account is not active. Please verify your email.", HttpStatusCode.PreconditionRequired);
            }

            var role = await _roleRepository.FindByIdAsync(user.RoleId, cancellationToken);
            var roleName = role?.Name ?? "Customer";

            var token = _jwtProvider.GenerateToken(user.PublicId, user.UserName, user.Email, roleName);

            var response = new AuthenticationResponse(user, token);

            return Result<AuthenticationResponse>
                .Succeed(response, "Login successfully.", HttpStatusCode.Accepted);
        }
    }
}
