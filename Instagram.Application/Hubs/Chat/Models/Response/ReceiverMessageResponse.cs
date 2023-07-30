using Instagram.Application.Common.Responses;

namespace Instagram.Application.Hubs.Chat.Models.Response;

public class ReceiverMessageResponse
{
    public GenericMessageResponse Message { get; set; } = null!;
    public GenericUserResponse Participant { get; set; } = null!;
    public string ChatName { get; set; } = null!;
    public Guid Id { get; set; }


}