using Instagram.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Instagram.Application.Common.Extensions;

public static class HttpContextUserExtensions
{

    public static UserId GetCurrentUserId(this ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity ?? throw new UnauthorizedAccessException();

        var userId = identity.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            throw new ArgumentNullException("User Id can't be parsed");
        }

        return new UserId(parsedUserId);
    }

}