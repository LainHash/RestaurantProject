using System.Threading.Tasks;

namespace Restaurant.Application.Services.Auth
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string toEmail, string token);
    }
}
