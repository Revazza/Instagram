using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Interfaces;

public interface IChatRepository : IGenericRepository<Chat, ChatId>
{
    Task<List<Chat>> GetChatsByUserIdAsync(UserId userId, int limit = 30);
}
