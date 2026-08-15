using System.ComponentModel.DataAnnotations;

namespace Restaurant.Contract.Settings.Email
{
    public sealed class EmailSettings
    {
        public const string SectionName = "Email";

        [Required]
        public string SmtpServer { get; init; } = string.Empty;

        [Range(1, 65535)]
        public int SmtpPort { get; init; }

        [Required]
        public string SenderName { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        public string SenderEmail { get; init; } = string.Empty;

        [Required]
        public string AppPassword { get; init; } = string.Empty;

        public bool EnableSsl { get; init; } = true;

        public int Timeout { get; init; } = 30000;
    }
}
