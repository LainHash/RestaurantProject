using System;

namespace Restaurant.Contract.DTOs.Auth
{
    public record RegisterRequest(string Email, string Password, string FullName);
    
    public record LoginRequest(string Email, string Password);
    
    public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);
    
    public record RefreshTokenRequest(string AccessToken, string RefreshToken);
    
    public record VerifyEmailRequest(string Email, string Token);
}
