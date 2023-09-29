using Instagram.Application.Commands.Chats.CreateChat;
using Instagram.Application.Commands.Chats.DeleteChat;
using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;
using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus.Models;
using Instagram.Application.Common.Extensions;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Queries.Chats.GetAllChats;
using Instagram.Application.Queries.Chats.GetChatById;
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

        [HttpPost("UpdateChatMessagesStatus")]
        public async Task<IActionResult> UpdateChatMessagesStatus(UpdateChatMessagesStatusRequest request)
        {
            var command = request.Adapt<UpdateChatMessagesStatusCommand>();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetChatById")]
        public async Task<IActionResult> GetChatById(Guid chatId)
        {
            var query = new GetChatByIdQuery(chatId.ToChatId());
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete("DeleteChat")]
        public async Task<IActionResult> DeleteChat()
        {
            var command = new DeleteChatCommand();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats()
        {
            var query = new GetAllChatsQuery(); 
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
