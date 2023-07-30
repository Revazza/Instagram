using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Hubs.Chat;

public class UserConnection
{
    public UserId UserId { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;
    public List<ChatId> ConnectedChatIds { get; set; }


    public UserConnection(UserId userId, string connectionId)
    {
        ConnectedChatIds = new List<ChatId>();
        UserId = userId;
        ConnectionId = connectionId;
    }

    public bool HasChatId(ChatId chatId)
    {
        return ConnectedChatIds.Contains(chatId);
    }

    public void AddChatId(ChatId chatId)
    {
        ConnectedChatIds.Add(chatId);
    }

}