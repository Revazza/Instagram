using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats;
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

    public ChatHub(
        IChatHubConnections chatConnections,
        IMediator mediator,
        IUserConnections userConnections)
    {
        _chatConnections = chatConnections;
        _mediator = mediator;
        _userConnections = userConnections;
    }

    public async Task JoinChat(Guid chatId)
    {
       var connectionId = Context.ConnectionId;
        var userConnection = _chatConnections.GetUserConnection(connectionId);
        var convertedChatId = new ChatId(chatId);

        if (userConnection is not null &&
            !userConnection.ConnectedChatIds.Contains(convertedChatId))
        {
            userConnection.ConnectedChatIds.Add(convertedChatId);
        }

        userConnection = new UserConnection(Context.User!.GetCurrentUserId());
        userConnection.ConnectedChatIds.Add(convertedChatId);
        await Groups.AddToGroupAsync(connectionId, chatId.ToString());

        _chatConnections.AddConnection(connectionId, userConnection);
    }

    public async Task SendMessage(string message, Guid chatId, Guid receiverUserId)
    {
        var a = Context.User.GetCurrentUserId();
        var userConnection = _chatConnections.GetUserConnection(Context.ConnectionId)!;

        var command = new AddMessageToChatCommand(chatId.ToChatId(), userConnection.UserId, message);
        var response = await _mediator.Send(command);

        if (response.Status != ResponseStatus.Ok)
        {
            throw new ArgumentException("Message couldn't delivered");
        }

        var newMessage = response.Get<Message>("newMessage")!.Adapt<GenericMessageResponse>();

        await Clients.Group(chatId.ToString())
            .ReceiveMessage(newMessage);

        var receiverConnectionId = _userConnections.GetConnectionId(receiverUserId.ToUserId());

        await Clients.Client(receiverConnectionId).ReceiveNotification("Hello");

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = _chatConnections.GetUserConnection(Context.ConnectionId);
        if (connection is not null)
        {
            foreach (var chatId in connection.ConnectedChatIds)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.Value.ToString());
            }
            _chatConnections.RemoveConnection(Context.ConnectionId);
        }

        await base.OnDisconnectedAsync(exception);

    }
}