using Instagram.Application.Common;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Instagram.Application.Queries.Chats.GetChatsByUserId;

public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, Response>
{
    private readonly IChatRepository _chatRepository;

    public GetChatsByUserIdQueryHandler(
        IChatRepository chatRepository
    )
    {
        _chatRepository = chatRepository;
    }

    public async Task<Response> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetChatsByUserIdAsync(request.UserId);

        var convertedChats = chats
            .Where(c => c.ChatMessages.Count != 0)
            .OrderByDescending(c => c.LastActivity)
            .Adapt<List<GenericChatResponse>>();

        return Response.Ok().Add("chats", convertedChats);
    }
}