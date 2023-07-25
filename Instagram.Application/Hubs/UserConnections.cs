using Instagram.Domain.Users;

namespace Instagram.Application.Hubs;

public interface IUserConnections
{
    bool AddConnection(string connectionId,UserId userId);
    bool DeleteConnection(string connectionId);
    string GetConnectionId(UserId userId);
}

public class UserConnections : IUserConnections
{
    public Dictionary<string, UserId> Connections { get; set; }

    public UserConnections()
    {
        Connections = new Dictionary<string, UserId>();
    }

    public bool AddConnection(string connectionId, UserId userId)
    {
        return Connections.TryAdd(connectionId, userId);
    }

    public bool DeleteConnection(string connectionId)
    {
        return Connections.Remove(connectionId);
    }

    public string GetConnectionId(UserId userId)
    {
        return Connections.FirstOrDefault(x => x.Value == userId).Key;
    }
}