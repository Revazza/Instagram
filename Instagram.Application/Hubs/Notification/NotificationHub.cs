using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Hubs.Notification;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
public class NotificationHub : Hub<INotificationClient>
{
    private readonly IUserConnections _userConnections;
    private readonly IUserRepository _userRepository;

    public NotificationHub(
        IUserConnections userConnections,
        IUserRepository userRepository)
    {
        _userConnections = userConnections;
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    public async Task NotifyNewMessageToUser(GenericMessageResponse message, Guid receiverUserId)
    {
        var receiverId = receiverUserId.ToUserId();
        var receiverConnectionId = _userConnections.GetConnectionId(receiverId);
        var receiver = await _userRepository.GetById(receiverId);

        if (receiver is null)
        {
            throw new Exception("Couldn't identify receiver");
        }
        await Clients.Client(receiverConnectionId).ReceiveMessageNotification(message);

    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User!.GetCurrentUserId();
        _userConnections.AddConnection(Context.ConnectionId, userId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _userConnections.DeleteConnection(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}