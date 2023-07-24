using Instagram.Domain.Chats;

namespace Instagram.Application.Hubs.Chat;


public interface IChatHubConnections
{
    bool AddConnection(string connectionId, UserConnection chatId);
    bool RemoveConnection(string connectionId);
    UserConnection? GetUserConnection(string connectionId);

}


public class ChatHubConnections : IChatHubConnections
{
    public Dictionary<string, UserConnection> Connections { get; set; }

    public ChatHubConnections()
    {
        Connections = new Dictionary<string, UserConnection>();
    }


    public bool AddConnection(string connectionId, UserConnection userConnection)
    {
        return Connections.TryAdd(connectionId, userConnection);
    }

    public bool RemoveConnection(string connectionId)
    {
        return Connections.Remove(connectionId);
    }


    public UserConnection? GetUserConnection(string connectionId)
    {
        Connections.TryGetValue(connectionId, out UserConnection? userConnection);
        return userConnection;
    }
}