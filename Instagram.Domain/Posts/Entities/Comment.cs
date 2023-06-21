using Instagram.Domain.Posts.ValueObjects;
using Instagram.Domain.Users;

namespace Instagram.Domain.Posts.Entities;

public class Comment
{
    public CommentId CommmentId { get; set; } = null!;
    public Post OriginalPost { get; set; } = null!;
    public PostId OriginalPostId { get; set; } = null!;
    public User CommentAuthor { get; set; } = null!;
    public UserId CommentAuthorId { get; set; } = null!;
    public string CommentDescription { get; set; } = string.Empty;


}