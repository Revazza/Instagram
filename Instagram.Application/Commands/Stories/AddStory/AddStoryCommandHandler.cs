using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Commands.Stories.AddStory;

public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand, Response>
{

    public async Task<Response> Handle(AddStoryCommand request, CancellationToken cancellationToken)
    {
        return Response.Ok();
    }
}