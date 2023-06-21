using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Posts.ValueObjects;
using Instagram.Domain.Users;

namespace Instagram.Domain.Posts;

public class Post
{
    public PostId PostId { get; set; } = null!;
    public User PostAuthor { get; set; } = null!;
    public UserId PostAuthorId { get; set; } = null!;
    public int TotalReactions { get; set; }
    public int TotalComments { get; set; }
    public string PostDescription { get; set; } = string.Empty;
    public IEnumerable<Comment> PostComments { get; set; }
    public IEnumerable<PostReaction> PostReactions { get; set; }

    public Post()
    {
        PostComments = new List<Comment>();
        PostReactions = new List<PostReaction>();
    }

}