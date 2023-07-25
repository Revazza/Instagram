using Instagram.Application.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Instagram.Application.Hubs.Notification;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
public class NotificationHub : Hub
{
    private readonly IUserConnections _userConnections;

    public NotificationHub(IUserConnections userConnections)
    {
        _userConnections = userConnections;
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