namespace Instagram.Application.Hubs.Chat.Models.Response;

public record UpdateChatMessagesStatusResponse(
    Guid ChatId,
    string Status);