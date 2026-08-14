using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtProvider jwtProvider,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
            {
                return Result<AuthResponse>.Fail("Invalid credentials or inactive account.", HttpStatusCode.Unauthorized);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return Result<AuthResponse>.Fail("Invalid credentials.", HttpStatusCode.Unauthorized);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return Result<AuthResponse>.Fail("Email is not verified.", HttpStatusCode.Forbidden);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtProvider.GenerateToken(user, roles);
            var refreshToken = _jwtProvider.GenerateRefreshToken();
            
            var expiryTime = DateTime.UtcNow.AddDays(7); 
            await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, expiryTime);

            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(60); 

            return Result<AuthResponse>.Succeed(new AuthResponse(accessToken, refreshToken, accessTokenExpiresAt), "Login successful");
        }

        public async Task<Result<bool>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result<bool>.Fail("Email is already taken.", HttpStatusCode.Conflict);
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Result<bool>.Fail("User registration failed.", HttpStatusCode.BadRequest);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailVerificationAsync(user.Email, token);

            return Result<bool>
                .Succeed(true, "Registration successful. Please check your email to verify your account.");
        }

        public async Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return Result<AuthResponse>.Fail("Invalid access token.", HttpStatusCode.Unauthorized);
            }

            var email = principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Result<AuthResponse>.Fail("Invalid access token claims.", HttpStatusCode.Unauthorized);
            }

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Result<AuthResponse>.Fail("Invalid or expired refresh token.", HttpStatusCode.Unauthorized);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _jwtProvider.GenerateToken(user, roles);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();
            
            var expiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateRefreshTokenAsync(user.Id, newRefreshToken, expiryTime);

            return Result<AuthResponse>.Succeed(new AuthResponse(newAccessToken, newRefreshToken, DateTime.UtcNow.AddMinutes(60)), "Token refreshed successfully");
        }

        public async Task<Result<bool>> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<bool>.Fail("User not found.", HttpStatusCode.NotFound);
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return Result<bool>.Fail("Email verification failed.", HttpStatusCode.BadRequest);
            }

            return Result<bool>.Succeed(true, "Email verified successfully");
        }
    }
}
