using Instagram.Application.Common;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using MediatR;

namespace Instagram.Application.Commands.Chats.AddMessageToChat;

public record AddMessageToChatCommand(
    ChatId ChatId,
    UserId SenderId,
    string Message) : IRequest<Response>;
