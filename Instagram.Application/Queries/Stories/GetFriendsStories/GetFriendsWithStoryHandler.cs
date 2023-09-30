using Instagram.Application.Commands.Users.CreateUser;
using Instagram.Application.Common;
using Instagram.Application.Common.Responses;
using Instagram.Application.Interfaces;
using Instagram.Application.Services;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Queries.Stories.GetFriendsStories;

public record GetFriendsWithStoryQuery() : IRequest<Response>;

public class GetFriendsWithStoryHandler : IRequestHandler<GetFriendsWithStoryQuery, Response>
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public GetFriendsWithStoryHandler(
        IUserService userService, 
        IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(GetFriendsWithStoryQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _userService.GetCurrentUserId();

        var friendsWithStories = await _userRepository
            .GetAll()
            .Where(x => x.Stories.Count() > 0)
            .ToListAsync();

        var response = friendsWithStories.Adapt<List<GenericUserResponse>>();

        return Response.Ok().Add("friendsWithStories", response);
    }
}