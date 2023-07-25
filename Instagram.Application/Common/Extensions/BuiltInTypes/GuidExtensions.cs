using Instagram.Domain.Chats;
using Instagram.Domain.Users;

namespace Instagram.Application.Common.Extensions.BuiltInTypes;

public static class GuidExtensions
{

    public static UserId ToUserId(this Guid guidUserId)
    {
        return new UserId(guidUserId);
    }

    public static ChatId ToChatId(this Guid chatId)
    {
        return new ChatId(chatId);
    }

}