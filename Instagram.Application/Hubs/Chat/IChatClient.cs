namespace Instagram.Application.Hubs.Chat;

public interface IChatClient
{
    Task UpdateChat(object? message);
}
