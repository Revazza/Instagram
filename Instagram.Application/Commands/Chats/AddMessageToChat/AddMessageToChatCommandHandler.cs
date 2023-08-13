using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Instagram.Application.Services;
using Instagram.Domain.Chats.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Instagram.Application.Commands.Chats.AddMessageToChat;

public class AddMessageToChatCommandHandler : IRequestHandler<AddMessageToChatCommand, GenericMessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserService _userService;

    public AddMessageToChatCommandHandler(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor contextAccessor,
        IUserService userService)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _dateTimeProvider = dateTimeProvider;
        _userService = userService;
    }

    public async Task<GenericMessageResponse> Handle(AddMessageToChatCommand request, CancellationToken cancellationToken)
    {
        var senderId = _userService.GetCurrentUserId();
        var chat = await _chatRepository.GetById(request.ChatId)
            ?? throw new ArgumentNullException("Chat can't be found");

        chat.LastActivity = _dateTimeProvider.UtcNow;

        var message = new Message()
        {
            MessageId = MessageId.Create(),
            SenderId = senderId,
            OriginalChatId = request.ChatId,
            MessageText = request.Message,
            CreatedAt = _dateTimeProvider.UtcNow,
            Status = request.isReceiverOnline ? MessageStatus.Delivered : MessageStatus.Sent,
        };

        _chatRepository.Update(chat);
        await _messageRepository.AddAsync(message);
        await _messageRepository.SaveChangesAsync();

        return message.Adapt<GenericMessageResponse>();
    }
}