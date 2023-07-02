namespace Instagram.Application.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int ExpirationMinutes { get; set; }

}