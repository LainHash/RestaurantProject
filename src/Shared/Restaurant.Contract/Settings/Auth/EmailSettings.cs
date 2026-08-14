namespace Restaurant.Contract.Settings.Auth
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";

        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string SenderName { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string AppPassword { get; set; } = null!;
    }
}
