using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats.Entities;
using Mapster;

namespace Instagram.Application.Mappings;

public class MessageMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Message, GenericMessageResponse>()
            .Map(dest => dest.MessageId, src => src.MessageId.Value)
            .Map(dest => dest.SenderId, src => src.SenderId.Value)
            .Map(dest => dest.OriginalChatId, src => src.OriginalChatId.Value)
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}