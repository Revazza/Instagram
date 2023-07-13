using Instagram.Application.Common;
using Instagram.Domain.Users;
using MediatR;

namespace Instagram.Application.Queries.Chats.GetChatsByUserId;

public class GetChatsByUserIdQuery : IRequest<Response>
{
    public UserId UserId { get; set; }
    public int Limit { get; set; }
    public GetChatsByUserIdQuery(Guid userId, int limit)
    {
        UserId = new UserId(userId);
        Limit = limit;
    }

}

