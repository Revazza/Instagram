using Instagram.Application.Common.Extensions;
using Instagram.Domain.Users;
using Microsoft.AspNetCore.Http;

namespace Instagram.Application.Services;

public interface IUserService
{
    UserId GetCurrentUserId();
}

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserId GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
    }

}