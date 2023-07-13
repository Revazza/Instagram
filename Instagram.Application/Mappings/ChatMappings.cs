using Instagram.Application.Common.Responses;
using Instagram.Application.Queries.Chats.GetChatsByUserId;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using Mapster;

namespace Instagram.Application.Mappings;

public class ChatMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Chat, GetChatsByUserIdResponse>()
            .Map(dest => dest.UserId, src => MapContext.Current!.Parameters["userId"]);
        config.NewConfig<User, GenericUserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
            //.Map(dest => dest.FullName, src => src.FullName)
            //.Map(dest => dest.UserName, src => src.UserName);
    }
}