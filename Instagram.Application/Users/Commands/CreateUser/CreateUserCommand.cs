using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    int Age) : IRequest<Response>;