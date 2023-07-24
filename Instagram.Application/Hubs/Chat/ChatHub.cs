using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Users;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Hubs.Chat;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
public class ChatHub : Hub<IChatClient>
{
    private readonly IChatHubConnections _connections;
    private readonly IMediator _mediator;

    public ChatHub(
        IChatHubConnections connections,
        IMediator mediator)
    {
        _connections = connections;
        _mediator = mediator;
    }

    public async Task JoinChat(Guid chatId)
    {
        var connectionId = Context.ConnectionId;
        var userConnection = _connections.GetUserConnection(connectionId);
        var convertedChatId = new ChatId(chatId);

        if (userConnection is not null &&
            !userConnection.ConnectedChatIds.Contains(convertedChatId))
        {
            userConnection.ConnectedChatIds.Add(convertedChatId);
        }

        userConnection = new UserConnection(Context.User!.GetCurrentUserId());
        userConnection.ConnectedChatIds.Add(convertedChatId);
        await Groups.AddToGroupAsync(connectionId, chatId.ToString());

        _connections.AddConnection(connectionId, userConnection);
    }

    public async Task SendMessage(string message, Guid chatId)
    {
        var userConnection = _connections.GetUserConnection(Context.ConnectionId)!;
        var convertedChatId = new ChatId(chatId);

        var command = new AddMessageToChatCommand(convertedChatId, userConnection.UserId, message);
        var response = await _mediator.Send(command);

        if (response.Status != ResponseStatus.Ok)
        {
            throw new ArgumentException("Message couldn't delivered");
        }

        var newMessage = response.Get<Message>("newMessage")!.Adapt<GenericMessageResponse>();

        await Clients.Group(chatId.ToString())
            .ReceiveMessage(newMessage);

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = _connections.GetUserConnection(Context.ConnectionId);
        if (connection is not null)
        {
            foreach (var chatId in connection.ConnectedChatIds)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.Value.ToString());
            }
            _connections.RemoveConnection(Context.ConnectionId);
        }

        await base.OnDisconnectedAsync(exception);

    }
}