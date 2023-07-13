namespace Instagram.Application.Commands.Users.CreateUser;

public record CreateUserResponse(
    string Id,
    string FullName,
    string Email);
