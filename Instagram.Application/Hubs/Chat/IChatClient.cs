namespace Instagram.Application.Hubs.Chat;

public interface IChatClient
{
    Task ReceiveMessage(object? message);
    Task ReceiveNotification(object? notification);
}
