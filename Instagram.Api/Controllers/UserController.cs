using Instagram.Application.Users.Commands.CreateUser;
using Instagram.Contracts.User.Requests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Api.Controllers
{
    [Route("User")]
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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserRequest, CreateUserCommand>(request);
            var response = await _mediator.Send(command);
            return Ok(response);
        }



    }
}
