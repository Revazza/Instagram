using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Hubs.Chat;
using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;

public class UpdateChatMessagesStatusCommandHandler : IRequestHandler<UpdateChatMessagesStatusCommand, Response>
{
    private readonly IChatRepository _chatRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;

    public UpdateChatMessagesStatusCommandHandler(
        IChatRepository chatRepository,
        IHttpContextAccessor contextAccessor,
        IHubContext<ChatHub, IChatClient> hubContext)
    {
        _chatRepository = chatRepository;
        _contextAccessor = contextAccessor;
        _hubContext = hubContext;
    }

    public async Task<Response> Handle(UpdateChatMessagesStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _contextAccessor.HttpContext?.User.GetCurrentUserId()
            ?? throw new UnauthorizedAccessException();

        Chat? chat = null;
        if (request.Status == MessageStatus.Seen)
        {
            chat = await _chatRepository.GetChatWithUnSeenMessages(request.ChatId.ToChatId());
        }

        if (chat is null)
        {
            return new Response().AddError($"Can't find chat by id {request.ChatId}");
        }

        var messages = chat.ChatMessages;
        if (!messages.Any())
        {
            return Response.Ok();
        }

        var lastMessage = messages.Last();

        if (lastMessage.SenderId == currentUserId)
        {
            return Response.Ok();
        }

        foreach (var message in messages)
        {
            if (message.SenderId == currentUserId)
            {
                continue;
            }
            message.Status = request.Status;
        }

        _chatRepository.Update(chat);
        await _chatRepository.SaveChangesAsync();

        return Response.Ok();
    }

}