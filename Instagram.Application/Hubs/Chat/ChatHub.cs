using Instagram.Application.Commands.Chats.AddMessageToChat;
using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;
using Instagram.Application.Common;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Hubs.Chat.Models.Requests;
using Instagram.Application.Hubs.Chat.Models.Response;
using Instagram.Application.Hubs.Notification;
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


    public async Task<GenericMessageResponse> SendMessage(SendMessageToUserRequest request)
    {
        var receiverId = request.ReceiverId.ToUserId();
        var receiverConnectionId = _userConnections.GetConnectionId(receiverId);
        var isReceiverOnline = receiverConnectionId != null;

        var newMessage = await AddMessageToChatAsync(request, isReceiverOnline);

        if (!isReceiverOnline)
        {
            return newMessage;
        }

        await _notificationHubContext.Clients
            .Client(receiverConnectionId!)
            .ReceiveMessageNotification(newMessage);

        var receiverChatConnectionId = _chatConnections.GetConnectionId(receiverId);
        if (receiverChatConnectionId is null)
        {
            return newMessage;
        }

        await Clients
            .Client(receiverChatConnectionId)
            .UpdateChat(newMessage);

        return newMessage;
    }

    public async Task<UpdateChatMessagesStatusResponse> UpdateChatMessagesStatus(UpdateChatMessagesStatusRequest request)
    {
        var command = new UpdateChatMessagesStatusCommand(request.ChatId, request.Status);
        var response = await _mediator.Send(command);
        
        if(response.Status != ResponseStatus.Ok)
        {
            throw new Exception("Error occured while updating message status");
        }
        var userResponse = new UpdateChatMessagesStatusResponse(request.ChatId,request.Status.ToString());

        var receiverConnectionId = _chatConnections.GetConnectionId(request.ReceiverId.ToUserId());

        if (receiverConnectionId is null)
        {
            return userResponse;
        }

        await Clients.Client(receiverConnectionId)
            .UpdateChatMessagesStatus(userResponse);

        return userResponse;
    }

    private async Task<GenericMessageResponse> AddMessageToChatAsync(SendMessageToUserRequest request, bool isReceiverOnline)
    {
        var command = new AddMessageToChatCommand(request.ChatId.ToChatId(), request.Message, isReceiverOnline);
        return await _mediator.Send(command);
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