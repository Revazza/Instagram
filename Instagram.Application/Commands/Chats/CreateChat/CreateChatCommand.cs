﻿using Instagram.Application.Common;
using Instagram.Domain.Users;
using MediatR;

namespace Instagram.Application.Commands.Chats.CreateChat;

public record CreateChatCommand(
    UserId ParticipantId,
    UserId CreatorId) : IRequest<Response>;