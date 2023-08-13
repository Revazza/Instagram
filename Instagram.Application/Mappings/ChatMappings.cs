using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus;
using Instagram.Application.Commands.Chats.UpdateChatMessagesStatus.Models;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Common.Responses;
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

    }
}