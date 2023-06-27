using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Queries.Login;

public record LoginUserCommand(
    string Email,
    string Password
) : IRequest<Response>;
