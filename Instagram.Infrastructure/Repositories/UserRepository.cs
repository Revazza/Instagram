using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using Instagram.Infrastructure.Db;

namespace Instagram.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(InstagramDbContext context) : base(context)
    {
    }



}