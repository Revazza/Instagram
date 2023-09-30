using Instagram.Domain.Stories;

namespace Instagram.Application.Interfaces;

public interface IStoryRepository : IGenericRepository<Story, StoryId>
{
}