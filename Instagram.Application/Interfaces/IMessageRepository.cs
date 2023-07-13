using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;

namespace Instagram.Application.Interfaces;

public interface IMessageRepository : IGenericRepository<Message, MessageId>
{
    Task<List<Message>> GetMessagesByChatIdAsync(ChatId chatId, int pageSize = 0, int messagesPerPage = 30);
}