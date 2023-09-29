namespace Instagram.Application.Common.Responses;

public class GenericChatResponse
{
    public Guid ChatId { get; set; }
    public string ChatName { get; set; } = string.Empty;
    public List<GenericUserResponse> Participants { get; set; }
    public List<GenericMessageResponse> ChatMessages { get; set; }
    public DateTime LastActivityAt { get; set; }

    public GenericChatResponse()
    {
        Participants = new List<GenericUserResponse>();
        ChatMessages = new List<GenericMessageResponse>();
    }

}