using Instagram.Domain.Chats.Entities;

namespace Instagram.Application.Commands.Chats.UpdateChatMessagesStatus.Models;

public record UpdateChatMessagesStatusRequest(
    Guid ChatId,
    MessageStatus Status);
