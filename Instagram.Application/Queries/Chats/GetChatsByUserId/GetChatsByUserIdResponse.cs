﻿using Instagram.Application.Common.Responses;

namespace Instagram.Application.Queries.Chats.GetChatsByUserId;
public class GetChatsByUserIdResponse
{
    public Guid ChatId { get; set; }
    public GenericUserResponse Participant { get; set; } = null!;
    public GenericMessageResponse? LastMessage { get; set; }
    public string ChatName { get; set; } = null!;
    public DateTime LastActivityAt { get; set; }
}