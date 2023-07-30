namespace Instagram.Application.Hubs.Chat.Models.Requests;

public record SendMessageToUserRequest(
    string Message,
    Guid ChatId,
    Guid ReceiverId,
    string SenderFullName,
    string SenderUserName,
    string ChatName);