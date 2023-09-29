using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Queries.Chats.GetChatById;

public record GetChatByIdQuery(ChatId ChatId) : IRequest<Response>;

public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, Response>
{
    private readonly IChatRepository _chatRepository;

    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Response> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetAll()
            .Include(c => c.Participants)
            .Include(c => c.ChatMessages)
            .FirstAsync();

        if (chat is null)
        {
            return Response.Error("Can't find chat by given id");
        }
        var response = chat.Adapt<GenericChatResponse>();
        return Response.Ok().Add("chat", response);
    }
}