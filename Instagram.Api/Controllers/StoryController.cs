using Instagram.Application.Commands.Stories.AddStory;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Api.Controllers;

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
    public IActionResult AddStory(IFormFile file)
    {
        var command = new AddStoryCommand(file);
        var response = _mediator.Send(command);
        return Ok(response);
    }

}
