namespace Instagram.Application.Authentication;

public class JwtSettings
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int ExpirationMinutes { get; set; }

}