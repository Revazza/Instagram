using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Instagram.Application.Services;
using Instagram.Domain.Stories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Queries.Stories.GetUserStoriesByStatus;

public record GetActiveStoriesByUserNameQuery(string UserName, StoryStatus Status = StoryStatus.Published) : IRequest<Response>;

public class GetActiveStoriesByUserNameQueryHandler : IRequestHandler<GetActiveStoriesByUserNameQuery, Response>
{
    private readonly IStoryRepository _storyRepository;
    public GetActiveStoriesByUserNameQueryHandler(
        IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<Response> Handle(GetActiveStoriesByUserNameQuery request, CancellationToken cancellationToken)
    {
        var stories = await _storyRepository.GetAll()
            .Include(x => x.Author)
            .Where(s => s.Author.UserName == request.UserName)
            .ToListAsync();
        var response = stories.Adapt<List<GenericStoryResponse>>();

        return Response.Ok().Add("stories", response);
    }
}