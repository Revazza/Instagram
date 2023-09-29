using Instagram.Application.Hubs.Chat.Models.Response;

namespace Instagram.Application.Hubs.Chat;

public interface IChatClient
{
    Task UpdateChat(object? message);
    Task UpdateChatMessagesStatus(UpdateChatMessagesStatusResponse response);
    Task AddNewChat(object? chat);
}
