using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.DomainEntities.Users.Queries.FilterUsersByUserName;

public record FilterUsersByUserNameQuery(
    string UserName) : IRequest<Response>;