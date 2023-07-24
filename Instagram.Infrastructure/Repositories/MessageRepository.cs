using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message, MessageId>, IMessageRepository
{
    public MessageRepository(InstagramDbContext context) : base(context)
    {
    }

    public async Task<List<Message>> GetMessagesByChatIdAsync(ChatId chatId, int pageSize = 0, int messagesPerPage = 30)
    {
        return await _context.Messages
            .Where(m => m.OriginalChatId == chatId)
            .OrderByDescending(m => m.CreatedAt)
            .Skip(pageSize * messagesPerPage)
            .Take(messagesPerPage)
            .ToListAsync();
    }

}