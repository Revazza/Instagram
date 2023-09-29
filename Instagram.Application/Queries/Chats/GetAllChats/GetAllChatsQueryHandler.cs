using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Queries.Chats.GetAllChats;

public record GetAllChatsQuery() : IRequest<Response>;

internal class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, Response>
{
    private readonly IChatRepository _chatRepository;

    public GetAllChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Response> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetAll().ToListAsync();

        return Response.Ok().Add("chats", chats);
    }
}