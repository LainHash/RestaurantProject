namespace Restaurant.Application.Services.Auth
{
    public interface IOtpHasher
    {
        string HashOtp(string otp);
        bool VerifyOtp(string inputOtp, string storedOtp);
    }
}
