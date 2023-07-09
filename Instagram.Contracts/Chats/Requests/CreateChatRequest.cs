namespace Instagram.Contracts.Chats.Requests;

public record CreateChatRequest(
    Guid ParticipantId,
    Guid CreatorId);
