namespace Instagram.Domain.Users.ValueObjects;

public record UserProfile(
    string FirstName,
    string LastName,
    string Email,
    int Age);
