using Instagram.Domain.Posts.ValueObjects;
using Instagram.Domain.Users;
using Instagram.Domain.Users.ValueObjects;

namespace Instagram.Domain.Posts.Entities;

public class PostReaction
{
    public PostReactionId PostReactionId { get; set; } = null!;
    public Post ReactedPost { get; set; } = null!;
    public PostId ReactedPostId { get; set; } = null!;
    public User ReactionAuthor { get; set; } = null!;
    public UserId ReactionAuthorId { get; set; } = null!;


}