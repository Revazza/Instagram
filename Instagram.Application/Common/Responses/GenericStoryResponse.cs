using Instagram.Domain.Stories.Entities;

namespace Instagram.Application.Common.Responses;

public class GenericStoryResponse
{
    public Guid Id { get; set; }
    public GenericUserResponse Author { get; set; } = null!;
    public int Duration { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; }
    public DateTime UploadDate { get; set; }
    //public List<StoryViewer> StoryViewers { get; set; }

    public GenericStoryResponse()
    {
        //StoryViewers = new List<StoryViewer>();
    }

}