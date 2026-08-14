namespace Restaurant.Contract.Settings.Auth
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
        public int RefreshTokenExpiryDays { get; set; }
    }
}
