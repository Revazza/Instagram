using Instagram.Application.Common;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using MediatR;

namespace Instagram.Application.Queries.Chats.GetChatWithMessages;

public record GetChatWithMessagesQuery(
    ChatId ChatId,
    UserId UserId) : IRequest<Response>;