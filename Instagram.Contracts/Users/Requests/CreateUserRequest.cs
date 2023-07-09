namespace Instagram.Contracts.Users.Requests;

public record CreateUserRequest(
    string Email,
    string UserName,
    string FullName,
    string Password);