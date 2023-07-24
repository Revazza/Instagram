using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Repositories;

public class ChatRepository : GenericRepository<Chat, ChatId>, IChatRepository
{
    public ChatRepository(InstagramDbContext context) : base(context)
    {
    }

    public async Task<List<Chat>> GetChatsByUserIdAsync(UserId userId, int limit = 30)
    {
        return await _context.Chats
            .Include(c => c.Participants)
            .Include(c => c.ChatMessages.OrderByDescending(cm => cm.CreatedAt).Take(1))
            .Where(c => c.Participants.Any(p => p.Id == userId))
            .OrderBy(c => c.LastActivity)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Chat?> GetChatWithParticipantsAsync(ChatId chatId)
    {

        return await _context.Chats
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.ChatId == chatId);
    }
}