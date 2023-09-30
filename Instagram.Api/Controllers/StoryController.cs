using Instagram.Application.Commands.Stories.AddStory;
using Instagram.Application.Commands.Stories.DeleteAllStories;
using Instagram.Application.Queries.Chats.GetAllChats;
using Instagram.Application.Queries.Stories.GetAllStories;
using Instagram.Application.Queries.Stories.GetFriendsStories;
using Instagram.Application.Queries.Stories.GetUserStoriesByStatus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
[Route("Story")]
[ApiController]
public class StoryController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public StoryController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("AddStory")]
    public async Task<IActionResult> AddStory(IFormFile file)
    {
        var command = new AddStoryCommand(file);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("GetAllStories")]
    public async Task<IActionResult> GetAllStories()
    {
        var query = new GetAllStoriesQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("GetActiveStoriesByUserName")]
    public async Task<IActionResult> GetActiveStoriesByUserName(string userName)
    {
        var query = new GetActiveStoriesByUserNameQuery(userName);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpDelete("DeleteAllStories")]
    public async Task<IActionResult> DeleteAllStories()
    {
        var command = new DeleteAllStoriesCommand();
        await _mediator.Send(command);
        return Ok();
    }

}
