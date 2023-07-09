using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Instagram.Application.DomainEntities.Chats.CreateChat;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Response>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;

    public CreateChatCommandHandler(
        IChatRepository chatRepository,
        IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(CreateChatCommand command, CancellationToken cancellationToken)
    {
        var query = _chatRepository.GetAll();
        var chat = await query.FirstOrDefaultAsync(s =>
            s.Participants.Any(p => p.Id == command.ParticipantId) &&
            s.Participants.Any(p => p.Id == command.CreatorId));

        if (chat != null)
        {
            return Response.Ok().Add("chatId", chat.ChatId.Value);
        }

        var chatParticipants = await _userRepository
            .GetAll()
            .Where(u => u.Id == command.CreatorId || u.Id == command.ParticipantId)
            .ToListAsync();

        var newChat = new Chat()
        {
            ChatId = ChatId.Create(),
            ChatName = "Chat",
            LastActivity = DateTime.UtcNow,
            Participants = chatParticipants
        };

        await _chatRepository.AddAsync(newChat);
        await _chatRepository.SaveChangesAsync();

        return Response.Ok().Add("chatId", newChat.ChatId.Value);
    }
}