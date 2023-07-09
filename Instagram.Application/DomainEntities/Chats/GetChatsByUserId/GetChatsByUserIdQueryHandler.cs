using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Instagram.Application.DomainEntities.Chats.GetChatsByUserId;

public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, Response>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;

    public GetChatsByUserIdQueryHandler(
        IChatRepository chatRepository,
        IMapper mapper)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetChatsByUserIdAsync(request.UserId, request.Limit);

        var convertedChats = chats
            .Select(s =>
            {
                var participant = s.Participants.First(p => p.Id != request.UserId);
                var lastMessage = s.ChatMessages.Any() ? s.ChatMessages.Last() : null;
                return new GetChatsByUserIdResponse()
                {
                    UserId = participant.Id.Value,
                    FullName = participant.FullName,
                    LastMessage = lastMessage?.MessageText ?? null,
                    LastMessageAuthorId = lastMessage?.SenderId.Value ?? null,
                    UserName = participant.UserName!
                };
            });

        return Response.Ok().Add("chats", convertedChats);
    }
}