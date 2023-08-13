using Instagram.Domain.Chats.Entities;

namespace Instagram.Application.Hubs.Chat.Models.Requests;

public record UpdateChatMessagesStatusRequest(
    Guid ChatId,
    MessageStatus Status,
    Guid ReceiverId);
