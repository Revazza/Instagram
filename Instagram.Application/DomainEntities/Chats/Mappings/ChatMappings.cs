using Instagram.Application.DomainEntities.Chats.GetChatsByUserId;
using Instagram.Domain.Chats;
using Mapster;

namespace Instagram.Application.DomainEntities.Chats.Mappings;

public class ChatMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Chat, GetChatsByUserIdResponse>()
            .Map(dest => dest.UserId, src => MapContext.Current!.Parameters["userId"]);
    }
}