using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Queries.FilterUsersByUserName;

public record FilterUsersByUserNameQuery(
    string UserName) : IRequest<Response>;