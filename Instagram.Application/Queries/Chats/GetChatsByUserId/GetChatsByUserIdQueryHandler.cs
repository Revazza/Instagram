﻿using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Instagram.Application.Queries.Chats.GetChatsByUserId;

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
            .Where(c => c.ChatMessages.Count != 0)
            .OrderByDescending(c => c.LastActivity)
            .Select(s =>
            {
                var participant = s.Participants.First(p => p.Id != request.UserId);
                var lastMessage = s.ChatMessages.Any() ? s.ChatMessages.Last() : null;
                return new GetChatsByUserIdResponse
                {
                    ChatId = s.ChatId.Value,
                    LastMessage = lastMessage?.MessageText ?? null,
                    ChatName = string.IsNullOrEmpty(s.ChatName) ? participant.UserName! : s.ChatName,
                    LastMessageAuthorId = lastMessage?.SenderId?.Value,
                    LastActivityAt = s.LastActivity,
                    Participant = participant.Adapt<GenericUserResponse>(),
                };
            });

        return Response.Ok().Add("chats", convertedChats);
    }
}