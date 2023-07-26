using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Hubs.Chat;

public class UserConnection
{
    public UserId UserId { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;
    public List<ChatId> ConnectedChatIds { get; set; }


    public UserConnection(UserId userId, string connectionId, ChatId chatId)
    {
        ConnectedChatIds = new List<ChatId>() { chatId };
        UserId = userId;
        ConnectionId = connectionId;
    }

}