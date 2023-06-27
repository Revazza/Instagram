namespace Instagram.Contracts.User.Requests;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    int Age);