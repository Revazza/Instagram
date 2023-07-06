namespace Instagram.Application.Users.Queries.FilterUsersByUserName;

public record FilterUsersByUserNameResponse(
    string Id,
    string UserName,
    string FullName);