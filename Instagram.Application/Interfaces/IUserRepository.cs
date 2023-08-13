using Instagram.Application.Common.Responses;
using Instagram.Domain.Users;

namespace Instagram.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User, UserId>
{
    Task<List<User>> FilterUsersByUserNameAsync(string userName);
    Task<UserIdentityDetails?> GetUserIdentityDetails(UserId userId);
}