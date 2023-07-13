using Instagram.Application.Commands.Chats.CreateChat;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Queries.Chats.GetChatsByUserId;
using Instagram.Application.Queries.Chats.GetChatWithMessages;
using Instagram.Contracts.Chats.Requests;
using Instagram.Domain.Chats;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("Chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ChatController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat(CreateChatRequest request)
        {
            var command = request.Adapt<CreateChatCommand>();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetUserChats")]
        public async Task<IActionResult> GetUserChats([FromQuery] Guid userId, int limit)
        {
            var query = new GetChatsByUserIdQuery(userId, limit);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetChatWithMessages")]
        public async Task<IActionResult> GetChatWithMessages(Guid chatId)
        {
            var query = new GetChatWithMessagesQuery(new ChatId(chatId), User.GetCurrentUserId());
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
