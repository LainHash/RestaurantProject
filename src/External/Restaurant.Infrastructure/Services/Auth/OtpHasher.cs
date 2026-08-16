using Microsoft.Extensions.Configuration;
using Restaurant.Application.Services.Auth;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Infrastructure.Services.Auth
{
    internal class OtpHasher : IOtpHasher
    {
        private readonly string _secret;

        public OtpHasher(IConfiguration configuration)
        {
            _secret = configuration["OTP_SECRET_KEY"]
                ?? throw new InvalidOperationException(
                    "OTP_SECRET_KEY is not configured.");
        }

        public string HashOtp(string otp)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secret));

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(otp));

            return Convert.ToHexString(hash);
        }

        public bool VerifyOtp(string otp, string storedHash)
        {
            using var hmac = new HMACSHA256(
                Encoding.UTF8.GetBytes(_secret));

            var actualHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(otp));

            var expectedHash = Convert.FromHexString(storedHash);

            return CryptographicOperations.FixedTimeEquals(
                actualHash,
                expectedHash);
        }
    }
}
