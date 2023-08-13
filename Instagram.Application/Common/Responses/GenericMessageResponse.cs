using Instagram.Domain.Chats.Entities;

namespace Instagram.Application.Common.Responses;

public record GenericMessageResponse(
    Guid MessageId,
    Guid OriginalChatId,
    Guid SenderId,
    string MessageText,
    DateTime CreatedAt,
    string Status);