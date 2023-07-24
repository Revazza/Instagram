using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Instagram.Application.Queries.Chats.GetChatWithMessages;

public class GetChatWithMessagesQueryHandler : IRequestHandler<GetChatWithMessagesQuery, Response>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;

    public GetChatWithMessagesQueryHandler(
        IChatRepository chatRepository,
        IMapper mapper,
        IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
        _messageRepository = messageRepository;
    }

    public async Task<Response> Handle(GetChatWithMessagesQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetChatWithParticipantsAsync(request.ChatId);
        if (chat is null)
        {
            return Response.Error("Can't identify chat");
        }

        if (!chat.Participants.Any(p => p.Id == request.UserId))
        {
            return Response.Error("You don't have access to that chat");
        }

        var participant = chat.Participants.First(p => p.Id != request.UserId).Adapt<GenericUserResponse>();
        var chatMessages = await _messageRepository.GetMessagesByChatIdAsync(request.ChatId);

        return Response.Ok()
            .Add("id",chat.ChatId.Value)
            .Add("chatName", chat.ChatName)
            .Add("participant", participant)
            .Add("chatMessages", chatMessages.Adapt<List<GenericMessageResponse>>());
    }

}