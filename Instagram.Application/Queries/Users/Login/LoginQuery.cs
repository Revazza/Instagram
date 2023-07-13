using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Queries.Users.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<Response>;