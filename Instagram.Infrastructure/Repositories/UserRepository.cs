using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(InstagramDbContext context) : base(context)
    {

    }

    public async Task<List<User>> FilterUsersByUserNameAsync(string userName)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.UserName!.ToLower().Contains(userName.ToLower()))
                .Take(50)
                .ToListAsync();
    }

    public async Task<UserIdentityDetails?> GetUserIdentityDetails(UserId userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new UserIdentityDetails
            {
                FullName = u.FullName,
                UserId = u.Id.Value,
                UserName = u.UserName!,
                Email = u.Email!
            }).FirstOrDefaultAsync();

    }
}