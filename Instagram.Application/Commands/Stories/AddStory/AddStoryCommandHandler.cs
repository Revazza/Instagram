using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Application.Services;
using Instagram.Domain.Stories;
using MediatR;

namespace Instagram.Application.Commands.Stories.AddStory;

public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand, Response>
{
    private readonly IStoryRepository _storyRepository;
    private readonly IUserService _userService;
    private readonly IDateTimeProvider _timeProvider;

    public AddStoryCommandHandler(
        IStoryRepository storyRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _storyRepository = storyRepository;
        _userService = userService;
        _timeProvider = timeProvider;
    }

    public async Task<Response> Handle(AddStoryCommand request, CancellationToken cancellationToken)
    {
        var file = request.File;
        var uploaderId = _userService.GetCurrentUserId();
        if (uploaderId is null)
        {
            return Response.Error("Coudldn't identify story author");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var story = new Story
        {
            StoryId = StoryId.Create(),
            AuthorId = uploaderId,
            Size = file.Length,
            Name = file.FileName,
            Duration = 5000,
            MediaType = file.ContentType,
            MediaData = memoryStream.ToArray(),
            UploadDate = _timeProvider.UtcNow,
        };

        await _storyRepository.AddAsync(story);
        await _storyRepository.SaveChangesAsync();
        return Response.Ok().Add("story", story);
    }
}