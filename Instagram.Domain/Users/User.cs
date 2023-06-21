﻿using Instagram.Domain.Chats;
using Instagram.Domain.Chats.Entities;
using Instagram.Domain.Followers;
using Instagram.Domain.Posts;
using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Users.Entities;
using Instagram.Domain.Users.ValueObjects;

namespace Instagram.Domain.Users;

public record UserId(Guid Value);

public class User
{
    public UserId UserId { get; set; } = null!;
    public UserProfile Profile { get; set; } = null!;
    public List<Post> Posts { get; set; }
    public List<Comment> UserComments { get; set; }
    public List<PostReaction> PostReactions { get; set; }
    public List<Follower> Followers { get; set; }
    public List<Follower> Followings { get; set; }
    public List<Chat> Chats { get; set; }
    public List<Message> Messages { get; set; }
    public List<FriendShip> Friends { get; set; }

    public User()
    {
        Friends = new List<FriendShip>();
        Messages = new List<Message>();
        Chats = new List<Chat>();
        UserComments = new List<Comment>();
        Posts = new List<Post>();
        PostReactions = new List<PostReaction>();
        Followers = new List<Follower>();
        Followings = new List<Follower>();
    }

}