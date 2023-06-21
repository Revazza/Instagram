using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Users;

namespace Instagram.Domain.Chats;

public record ChatId(Guid Value);

public class Chat
{
    public ChatId ChatId { get; set; } = null!;
    public string ChatName { get; set; } = string.Empty;
    public List<User> Participants { get; set; }
    public IEnumerable<Message> ChatMessages { get; set; }

    //TODO:
    //ChatSettings should be added

    public Chat()
    {
        ChatMessages = new List<Message>();
    }

}