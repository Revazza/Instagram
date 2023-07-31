using Instagram.Domain.Users;

namespace Instagram.Application.Hubs.Chat;


public interface IChatHubConnections
{
    bool AddConnection(string connectionId, UserId userId);
    bool RemoveConnection(string connectionId);
    UserId? GetUserId(string connectionId);
    string? GetConnectionId(UserId userId);
    public bool ConnectionExists(UserId userId);
    public bool ConnectionExists(string connectionId);
}


public class ChatHubConnections : IChatHubConnections
{
    public Dictionary<string, UserId> Connections { get; set; }

    public ChatHubConnections()
    {
        Connections = new Dictionary<string, UserId>();
    }


    public bool AddConnection(string connectionId, UserId userId)
    {
        return Connections.TryAdd(connectionId, userId);
    }

    public bool RemoveConnection(string connectionId)
    {
        return Connections.Remove(connectionId);
    }

    public UserId? GetUserId(string connectionId)
    {
        Connections.TryGetValue(connectionId, out var userId);
        return userId;
    }

    public string? GetConnectionId(UserId userId)
    {
        return Connections.FirstOrDefault(s => s.Value == userId).Key;
    }

    public bool ConnectionExists(UserId userId)
    {
        return Connections.ContainsValue(userId);
    }

    public bool ConnectionExists(string connectionId)
    {
        return Connections.ContainsKey(connectionId);
    }

}