using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Hubs.Chat.Models.Requests;
using Instagram.Application.Hubs.Chat.Models.Response;
using Instagram.Application.Hubs.Notification;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task JoinChat(Guid chatId)
    {
        var connectionId = Context.ConnectionId;
        var userConnection = _chatConnections.GetUserConnection(connectionId);
        var convertedChatId = new ChatId(chatId);

        if (userConnection is not null &&
            userConnection.HasChatId(convertedChatId))
        {
            return;
        }

        userConnection ??= new UserConnection(
            Context.User!.GetCurrentUserId(),
            connectionId);

        userConnection.AddChatId(convertedChatId);

        await Groups.AddToGroupAsync(connectionId, chatId.ToString());

        _chatConnections.AddConnection(connectionId, userConnection);
    }

    public async Task SendMessage(SendMessageToUserRequest request)
    {
        var chatId = request.ChatId.ToChatId();
        var receiverId = request.ReceiverId.ToUserId();
        var currentUserConnection = _chatConnections.GetUserConnection(Context.ConnectionId)!;

        var command = new AddMessageToChatCommand(chatId, currentUserConnection.UserId, request.Message);
        var response = await _mediator.Send(command);
        var a = DateTime.UtcNow;
        if (response.Status != ResponseStatus.Ok)
        {
            throw new ArgumentException("Message couldn't be delivered");
        }

        var newMessage = response.Get<Message>("newMessage")!.Adapt<GenericMessageResponse>();

        await Clients.Group(chatId.Value.ToString())
            .ReceiveMessage(newMessage);

        var receiverConnectionId = _userConnections.GetConnectionId(receiverId);

        if (receiverConnectionId is null)
        {
            return;
        }

        await _notificationHubContext.Clients
            .Client(receiverConnectionId)
            .ReceiveMessageNotification(newMessage);

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = _chatConnections.GetUserConnection(Context.ConnectionId);
        if (connection is null)
        {
            return;
        }

        foreach (var chatId in connection.ConnectedChatIds)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.Value.ToString());
        }
        _chatConnections.RemoveConnection(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);

    }

    public override Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var currentUserId = Context.User?.GetCurrentUserId()
            ?? throw new UnauthorizedAccessException(nameof(OnConnectedAsync));


        return base.OnConnectedAsync();
    }

}