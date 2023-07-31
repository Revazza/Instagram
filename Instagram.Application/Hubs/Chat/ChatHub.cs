using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Hubs.Chat.Models.Requests;
using Instagram.Application.Hubs.Notification;
using Instagram.Domain.Chats.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Hubs.Chat;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
public class ChatHub : Hub<IChatClient>
{
    private readonly IChatHubConnections _chatConnections;
    private readonly IUserConnections _userConnections;
    private readonly IMediator _mediator;
    private readonly IHubContext<NotificationHub, INotificationClient> _notificationHubContext;

    public ChatHub(
        IChatHubConnections chatConnections,
        IMediator mediator,
        IUserConnections userConnections,
        IHubContext<NotificationHub, INotificationClient> notificationHubContext)
    {
        _chatConnections = chatConnections;
        _mediator = mediator;
        _userConnections = userConnections;
        _notificationHubContext = notificationHubContext;
    }
    //senderCOn - GUbwWIjUaLPGd_e_VWpB-g
    public async Task SendMessage(SendMessageToUserRequest request)
    {
        var chatId = request.ChatId.ToChatId();
        var receiverId = request.ReceiverId.ToUserId();
        var senderConnectionId = Context.ConnectionId;
        var senderUserId = _chatConnections.GetUserId(senderConnectionId)!;

        var command = new AddMessageToChatCommand(chatId, senderUserId, request.Message);
        var response = await _mediator.Send(command);
        if (response.Status != ResponseStatus.Ok)
        {
            throw new ArgumentException("Message couldn't be delivered");
        }

        var newMessage = response.Get<Message>("newMessage")!.Adapt<GenericMessageResponse>();

        await Clients.Client(senderConnectionId)
            .UpdateChat(newMessage);

        var receiverConnectionId = _chatConnections.GetConnectionId(receiverId);
        if (receiverConnectionId is not null)
        {
            await Clients.Client(receiverConnectionId)
                .UpdateChat(newMessage);
        }

        receiverConnectionId = _userConnections.GetConnectionId(receiverId);
        if (receiverConnectionId is not null)
        {
            await _notificationHubContext.Clients
                .Client(receiverConnectionId)
                .ReceiveMessageNotification(newMessage);
        }


    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _chatConnections.RemoveConnection(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);

    }

    public override Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var currentUserId = Context.User?.GetCurrentUserId()
            ?? throw new UnauthorizedAccessException(nameof(OnConnectedAsync));

        _chatConnections.AddConnection(connectionId, currentUserId);

        return base.OnConnectedAsync();

    }

}