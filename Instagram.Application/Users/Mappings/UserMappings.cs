using Instagram.Application.Users.Commands.CreateUser;
using Instagram.Application.Users.Queries.FilterUsersByUserName;
using Instagram.Domain.Users;
using Instagram.Domain.Users.ValueObjects;
using Mapster;

namespace Instagram.Application.Users.Mappings;

public class UserMappings : IRegister
{

    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateUserCommand, UserProfile>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email);

        config.NewConfig<CreateUserCommand, User>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.UserName, src => src.UserName);

        config.NewConfig<User, CreateUserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<User, FilterUsersByUserNameResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

    }

}