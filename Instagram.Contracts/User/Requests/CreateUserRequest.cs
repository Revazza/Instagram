namespace Instagram.Contracts.User.Requests;

public record CreateUserRequest(
    string Email,
    string UserName,
    string FullName,
    string Password);