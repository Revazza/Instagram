namespace Instagram.Application.Queries.Users.FilterUsersByUserName;

public record FilterUsersByUserNameResponse(
    string Id,
    string UserName,
    string FullName);