namespace BugTrackingSystem.Domain.Common
{
    public class JwtConfig
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Secret { get; set; }
        public string? ExpiresMinutes { get; set; }
    }
}
