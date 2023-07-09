using Instagram.Application.DomainEntities.Chats.CreateChat;
using Instagram.Contracts.Chats.Requests;
using Instagram.Domain.Users;
using Mapster;

namespace Instagram.Api.Mappings;

public class ChatMappingsConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChatRequest, CreateChatCommand>()
            .Map(dest => dest.CreatorId, src => new UserId(src.CreatorId))
            .Map(dest => dest.ParticipantId, src => new UserId(src.ParticipantId));
    }


}
