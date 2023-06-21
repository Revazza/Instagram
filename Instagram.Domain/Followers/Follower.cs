using Instagram.Domain.Users;

namespace Instagram.Domain.Followers;

public record FollowId(Guid Value);

public class Follower
{
    public FollowId FollowId { get; set; } = null!;
    public User FollowedUser { get; set; } = null!;
    public UserId FollowedUserId { get; set; } = null!;
    public User UserFollowing { get; set; } = null!;
    public UserId UserFollowingId { get; set; } = null!;


}