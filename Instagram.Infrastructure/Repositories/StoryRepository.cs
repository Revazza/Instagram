using Instagram.Application.Interfaces;
using Instagram.Domain.Stories;
using Instagram.Infrastructure.Db;

namespace Instagram.Infrastructure.Repositories;

internal class StoryRepository : GenericRepository<Story, StoryId>, IStoryRepository
{
    public StoryRepository(InstagramDbContext context) : base(context)
    {
    }

}