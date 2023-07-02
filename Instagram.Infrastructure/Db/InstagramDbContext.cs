using Instagram.Domain.Followers;
using Instagram.Domain.Posts;
using Instagram.Domain.Posts.Entities;
using Instagram.Domain.Users;
using Instagram.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Db;
    
public class InstagramDbContext : IdentityDbContext<User, UserRole, UserId>
{
    public const string ConnectionString = "InstagramDbContext";
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<Follower> Followers { get; set; }


    public InstagramDbContext(DbContextOptions<InstagramDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstagramDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
}