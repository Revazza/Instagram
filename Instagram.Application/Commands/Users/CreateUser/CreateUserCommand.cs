using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Commands.Users.CreateUser;

public record CreateUserCommand(
    string Email,
    string UserName,
    string FullName,
    string Password) : IRequest<Response>;