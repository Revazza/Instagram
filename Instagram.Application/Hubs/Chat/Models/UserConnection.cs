using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Hubs.Chat;

public class UserConnection
{
    public UserId UserId { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;

    public UserConnection(UserId userId, string connectionId)
    {
        UserId = userId;
        ConnectionId = connectionId;
    }

}