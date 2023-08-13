using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Interfaces;

public interface IChatRepository : IGenericRepository<Chat, ChatId>
{
    Task<List<Chat>> GetChatsByUserIdAsync(UserId userId);
    Task<Chat?> GetChatWithParticipantsAsync(ChatId chatId);
    Task<Chat?> GetChatWithMessages(ChatId chatId);
    Task<Chat?> GetChatWithUnSeenMessages(ChatId userId);

}
