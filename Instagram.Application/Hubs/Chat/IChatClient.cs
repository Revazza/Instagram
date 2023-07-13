namespace Instagram.Application.Hubs.Chat;

public interface IChatClient
{
    Task ReceiveMessage(object? message);
}
