namespace Instagram.Domain.Users.Entities;

public record FriendShipId(Guid Value);

public class FriendShip
{
    public FriendShipId FriendShipId { get; set; } = null!;
    public User Friend { get; set; } = null!;
    public UserId FriendId { get; set; } = null!;

}