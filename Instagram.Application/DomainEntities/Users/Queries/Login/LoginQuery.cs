using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.DomainEntities.Users.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<Response>;