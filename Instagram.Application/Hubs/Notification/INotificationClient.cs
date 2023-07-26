using Instagram.Application.Common.Responses;

namespace Instagram.Application.Hubs.Notification;
public interface INotificationClient
{
    Task ReceiveMessageNotification(GenericMessageResponse message);
}
