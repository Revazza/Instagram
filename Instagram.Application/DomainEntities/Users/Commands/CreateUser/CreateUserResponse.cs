namespace Instagram.Application.DomainEntities.Users.Commands.CreateUser;

public record CreateUserResponse(
    string Id,
    string FullName,
    string Email);
