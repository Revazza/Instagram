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
            .Where(u => u.UserName!.ToLower().Contains(userName))
                .Take(50)
                .ToListAsync();
    }

}