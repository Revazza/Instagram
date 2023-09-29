using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats;
using MediatR;

namespace Instagram.Application.Commands.Chats.AddMessageToChat;

public record AddMessageToChatCommand(
    ChatId ChatId,
    string Message,
    bool IsReceiverOnline) : IRequest<GenericChatResponse>;
