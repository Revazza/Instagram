using Instagram.Domain.Chats;

namespace Instagram.Application.Hubs.Chat;


public interface IChatHubConnections
{
    bool AddConnection(string connectionId, ChatId chatId);
    bool RemoveConnection(string connectionId);
    bool IsConnected(string connection);
    ChatId? GetChatId(string connectionId);

}


public class ChatHubConnections : IChatHubConnections
{
    public Dictionary<string, ChatId> Connections { get; set; }

    public ChatHubConnections()
    {
        Connections = new Dictionary<string, ChatId>();
    }

    public bool AddConnection(string connectionId, ChatId chatId)
    {
        return Connections.TryAdd(connectionId, chatId);
    }

    public ChatId? GetChatId(string connectionId)
    {
        Connections.TryGetValue(connectionId, out ChatId? chatId);
        return chatId;
    }

    public bool RemoveConnection(string connectionId)
    {
        return Connections.Remove(connectionId); ;
    }

    public bool IsConnected(string connection)
    {
        throw new NotImplementedException();
    }

}