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

    public async Task<User?> FindByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(s => s.Email == email);
    }
}