using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Instagram.Domain.Users;
using Instagram.Application.Services;

namespace Instagram.Application.Authentication;

public interface IJwtTokenGenerator
{
    public string Generate(User user);
}


public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _settings;
    public JwtTokenGenerator(
        IOptions<JwtSettings> settings,
        IDateTimeProvider dateTimeProvider)
    {
        _settings = settings.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.Value.ToString()),
            new Claim("fullName",user.FullName),
            new Claim("userName",user.UserName!),
            new Claim(ClaimTypes.Role,"user")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_settings.ExpirationMinutes),
            signingCredentials: credentials);

        var tokenGenerator = new JwtSecurityTokenHandler();
        var jwtString = tokenGenerator.WriteToken(token);
        return jwtString;
    }

}