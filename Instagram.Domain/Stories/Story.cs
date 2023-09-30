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

public enum StoryStatus
{
    Published,
    Expires
}

public class Story
{
    public StoryId StoryId { get; set; } = null!;
    public User Author { get; set; } = null!;
    public UserId AuthorId { get; set; } = null!;
    public int Duration { get; set; }
    public string MediaType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    //public StoryStatus Status { get; set; }
    public byte[] MediaData { get; set; }
    public DateTime UploadDate { get; set; }
    public List<StoryViewer> StoryViewers { get; set; }

    public Story()
    {
        StoryViewers = new List<StoryViewer>();
        MediaData = Array.Empty<byte>();
    }


}