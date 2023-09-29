using Instagram.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Instagram.Application.Commands.Stories.AddStory;

public record AddStoryCommand(IFormFile File) : IRequest<Response>;
