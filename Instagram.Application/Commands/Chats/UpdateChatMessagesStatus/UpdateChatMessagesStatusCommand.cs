using Instagram.Application.Common;
using Instagram.Domain.Chats.Entities;
using MediatR;

namespace Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;

public record UpdateChatMessagesStatusCommand(
    Guid ChatId,
    MessageStatus Status) : IRequest<Response>;
