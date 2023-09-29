using Instagram.Domain.Stories.Entities;
using Instagram.Domain.Users;

namespace Instagram.Domain.Stories;

public record StoryId(Guid Value)
{
    public static StoryId Create()
    {
        return new StoryId(Guid.NewGuid());
    }
}

public class Story
{
    public StoryId StoryId { get; set; } = null!;
    public User Author { get; set; } = null!;
    public UserId AuthorId { get; set; } = null!;
    public int Duration { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public int FileSize { get; set; }
    public List<StoryViewer> StoryViewers { get; set; }

    public Story()
    {
        StoryViewers = new List<StoryViewer>();
    }


}