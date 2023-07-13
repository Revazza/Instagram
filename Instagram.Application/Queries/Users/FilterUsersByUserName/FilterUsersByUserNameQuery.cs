using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Queries.Users.FilterUsersByUserName;

public record FilterUsersByUserNameQuery(
    string UserName) : IRequest<Response>;