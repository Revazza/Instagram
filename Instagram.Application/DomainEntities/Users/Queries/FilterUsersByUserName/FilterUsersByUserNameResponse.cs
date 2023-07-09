namespace Instagram.Application.DomainEntities.Users.Queries.FilterUsersByUserName;

public record FilterUsersByUserNameResponse(
    string Id,
    string UserName,
    string FullName);