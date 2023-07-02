using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string UserName,
    string FullName,
    string Password) : IRequest<Response>;