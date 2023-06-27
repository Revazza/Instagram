using Instagram.Domain.Users;

namespace Instagram.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User, UserId>
{

}