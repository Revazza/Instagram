namespace Instagram.Application.DomainEntities.Chats.GetChatsByUserId;

public class GetChatsByUserIdResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? LastMessage { get; set; }
    public Guid? LastMessageAuthorId { get; set; }

}