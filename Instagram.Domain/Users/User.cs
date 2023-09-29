using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Followers;
using Instagram.Domain.Posts;
using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Stories;
using Instagram.Domain.Stories.Entities;
using Instagram.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Domain.Users;

public record UserId(Guid Value)
{
    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }
}


public class User : IdentityUser<UserId>
{
    public string FullName { get; set; } = null!;
    public List<Post> Posts { get; set; }
    public List<Comment> UserComments { get; set; }
    public List<PostReaction> PostReactions { get; set; }
    public List<Follower> Followers { get; set; }
    public List<Follower> Followings { get; set; }
    public List<Chat> Chats { get; set; }
    public List<Message> Messages { get; set; }
    public List<FriendShip> Friends { get; set; }
    public List<Story> Stories { get; set; }
    public List<StoryViewer> ViewedStories { get; set; }


    public User()
    {
        Id = UserId.Create();
        Friends = new List<FriendShip>();
        Messages = new List<Message>();
        Chats = new List<Chat>();
        UserComments = new List<Comment>();
        Posts = new List<Post>();
        PostReactions = new List<PostReaction>();
        Followers = new List<Follower>();
        Followings = new List<Follower>();
        Stories = new List<Story>();
        ViewedStories = new List<StoryViewer>(); 
        UserName = null!;
    }

}