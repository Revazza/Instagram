using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Chats.Entities;
using MediatR;

namespace Instagram.Application.Commands.Chats.AddMessageToChat;

public class AddMessageToChatCommandHandler : IRequestHandler<AddMessageToChatCommand, Response>
{
    private readonly IMessageRepository _messageRepository;

    public AddMessageToChatCommandHandler(
        IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Response> Handle(AddMessageToChatCommand request, CancellationToken cancellationToken)
    {
        var message = new Message()
        {
            MessageId = MessageId.Create(),
            SenderId = request.SenderId,
            OriginalChatId = request.ChatId,
            MessageText = request.Message,
        };

        await _messageRepository.AddAsync(message);
        await _messageRepository.SaveChangesAsync();

        return Response.Ok().Add("newMessage", message);
    }
}