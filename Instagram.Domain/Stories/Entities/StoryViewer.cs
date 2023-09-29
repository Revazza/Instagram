using Instagram.Domain.Users;

namespace Instagram.Domain.Stories.Entities;

public class StoryViewer
{
    public StoryId ViewedStoryId { get; set; } = null!;
    public Story ViewedStory { get; set; } = null!;
    public User Viewer { get; set; } = null!;
    public UserId ViewerId { get; set; } = null!;

}