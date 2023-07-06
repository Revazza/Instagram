namespace Instagram.Contracts.User.Requests;

public record LoginRequest(
    string Email,
    string Password);
