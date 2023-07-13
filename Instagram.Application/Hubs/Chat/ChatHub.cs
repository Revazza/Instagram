using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Users;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Hubs.Chat;


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
        await Groups.AddToGroupAsync(connectionId, chatId.ToString());
        _connections.AddConnection(connectionId, new ChatId(chatId));
    }

    public async Task SendMessage(string message, Guid userId)
    {
        var convertedUserId = new UserId(userId);

        var chatId = _connections.GetChatId(Context.ConnectionId)
            ?? throw new ArgumentException("Couldn't identify chat connection");

        var command = new AddMessageToChatCommand(chatId, convertedUserId, message);
        var response = await _mediator.Send(command);
        if (response.Status != ResponseStatus.Ok)
        {
            throw new ArgumentException("Message couldn't delivered");
        }

        object? newMessage = response.Get<Message>("newMessage");

        if(newMessage is not null)
        {
            newMessage = ((Message)newMessage).Adapt<GenericMessageResponse>();
        }
 
        await Clients.Group(chatId.Value.ToString())
            .ReceiveMessage(newMessage);

    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.RemoveConnection(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}