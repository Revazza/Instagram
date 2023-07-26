using Instagram.Application.Common.Responses;

namespace Instagram.Application.Hubs.Notification.Models;

public class NotifyNewMessageResponse
{
    public GenericUserResponse User { get; set; } = null!;
    public GenericMessageResponse Message { get; set; } = null!;

}