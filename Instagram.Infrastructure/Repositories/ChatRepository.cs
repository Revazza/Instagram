using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Users;
using Instagram.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Repositories;

public class ChatRepository : GenericRepository<Chat, ChatId>, IChatRepository
{
    private const int LIMIT = 30;
    public ChatRepository(InstagramDbContext context) : base(context)
    {
    }


    public async Task<List<Chat>> GetChatsByUserIdAsync(UserId userId)
    {
        return await _context.Chats
            .AsNoTracking()
            .Include(c => c.Participants)
            .Include(c => c.ChatMessages.OrderByDescending(cm => cm.CreatedAt).Take(1))
            .Where(c => c.Participants.Any(p => p.Id == userId))
            .OrderByDescending(c => c.LastActivity)
            .Take(LIMIT)
            .ToListAsync();
    }

    public async Task<Chat?> GetChatWithMessages(ChatId chatId)
    {
        return await _context.Chats
            .AsNoTracking()
            .Include(c => c.ChatMessages)
            .FirstOrDefaultAsync();
    }

    public async Task<Chat?> GetChatWithParticipantsAsync(ChatId chatId)
    {

        return await _context.Chats
            .AsNoTracking()
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.ChatId == chatId);
    }

    public async Task<Chat?> GetChatWithUnSeenMessages(ChatId chatId)
    {
        return await _context.Chats
            .Include(c => c.ChatMessages.Where(cm => cm.Status != MessageStatus.Seen))
            .FirstOrDefaultAsync(c => c.ChatId == chatId);
    }
}