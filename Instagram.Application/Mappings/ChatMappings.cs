using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;
using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus.Models;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using Mapster;

namespace Instagram.Application.Mappings;

public class ChatMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GenericUserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<UpdateChatMessagesStatusRequest, UpdateChatMessagesStatusCommand>()
            .Map(dest => dest.ChatId, src => src.ChatId.ToChatId());
        config.NewConfig<Chat, GenericChatResponse>()
            .Map(dest => dest.ChatId, src => src.ChatId.Value)
            .Map(dest => dest.Participants, src => src.Participants.Adapt<List<GenericUserResponse>>())
            .Map(dest => dest.ChatMessages, src => src.ChatMessages.Adapt<List<GenericMessageResponse>>())
            .Map(dest => dest.LastActivityAt, src =>src.LastActivity);


    }
}