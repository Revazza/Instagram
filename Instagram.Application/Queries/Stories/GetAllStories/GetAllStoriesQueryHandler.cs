using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace Instagram.Application.Queries.Stories.GetAllStories;

public record GetAllStoriesQuery() : IRequest<Response>;

public class GetAllStoriesQueryHandler : IRequestHandler<GetAllStoriesQuery, Response>
{
    private readonly IStoryRepository _storyRepository;

    public GetAllStoriesQueryHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<Response> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
    {
        var stories = await _storyRepository.GetAll().ToListAsync();

        return Response.Ok().Add("stories", stories);
    }
}