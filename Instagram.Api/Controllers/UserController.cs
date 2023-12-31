﻿using Instagram.Application.Commands.Users.CreateUser;
using Instagram.Application.Queries.Stories.GetFriendsStories;
using Instagram.Application.Queries.Users.FilterUsersByUserName;
using Instagram.Application.Queries.Users.Login;
using Instagram.Contracts.Users.Requests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Api.Controllers
{
    [Route("User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public UserController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserRequest, CreateUserCommand>(request);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginRequest, LoginQuery>(request);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("FilterUsersByUserName")]
        public async Task<IActionResult> GetUsersByUserName([FromQuery] string userName)
        {
            var query = new FilterUsersByUserNameQuery(userName);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetFriendsWithStory")]
        public async Task<IActionResult> GetFriendsWithStory()
        {
            var query = new GetFriendsWithStoryQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
            
    }
}
