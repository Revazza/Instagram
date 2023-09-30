using Instagram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Commands.Stories.DeleteAllStories;


public record DeleteAllStoriesCommand() : IRequest;

public class DeleteAllStoriesCommandHandler : IRequestHandler<DeleteAllStoriesCommand>
{
    private readonly IStoryRepository _storyRepository;

    public DeleteAllStoriesCommandHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task Handle(DeleteAllStoriesCommand request, CancellationToken cancellationToken)
    {
        var stories = await _storyRepository.GetAll().ToListAsync();

        foreach (var story in stories)
        {
            _storyRepository.Delete(story);
        }

        await _storyRepository.SaveChangesAsync();
    }
}