using Instagram.Domain.Users;

namespace Instagram.Domain.Chats.Entities;

public record MessageId(Guid Value)
{
    public static MessageId Create()
    {
        return new MessageId(Guid.NewGuid());
    }
}

public class Message
{
    public MessageId MessageId { get; set; } = null!;
    public Chat OriginalChat { get; set; } = null!;
    public ChatId OriginalChatId { get; set; } = null!;
    public User Sender { get; set; } = null!;
    public UserId SenderId { get; set; } = null!;
    public string MessageText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}