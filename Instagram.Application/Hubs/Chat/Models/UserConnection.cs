using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Hubs.Chat;

public class UserConnection
{
    public UserId UserId { get; set; } = null!;
    public List<ChatId> ConnectedChatIds { get; set; }


    public UserConnection(UserId userId)
    {
        ConnectedChatIds = new List<ChatId>();
        UserId = userId;
    }

}