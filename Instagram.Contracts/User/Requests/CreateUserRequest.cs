namespace Instagram.Contracts.User.Requests;

public record CreateUserRequest(
    string Email,
    string FullName,
    string Password);