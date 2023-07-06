using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<Response>;