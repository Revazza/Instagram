using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Application.Services;
using Instagram.Domain.Chats.Entities;
using MediatR;

namespace Instagram.Application.Commands.Chats.AddMessageToChat;

public class AddMessageToChatCommandHandler : IRequestHandler<AddMessageToChatCommand, Response>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddMessageToChatCommandHandler(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response> Handle(AddMessageToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetById(request.ChatId)
            ?? throw new ArgumentNullException("Chat can't be found");

        chat.LastActivity = _dateTimeProvider.UtcNow;

        var message = new Message()
        {
            MessageId = MessageId.Create(),
            SenderId = request.SenderId,
            OriginalChatId = request.ChatId,
            MessageText = request.Message,
            CreatedAt = _dateTimeProvider.UtcNow,
        };

        _chatRepository.Update(chat);
        await _messageRepository.AddAsync(message);
        await _messageRepository.SaveChangesAsync();

        return Response.Ok().Add("newMessage", message);
    }
}