using Instagram.Application.Users.Commands.CreateUser;
using Instagram.Contracts.User.Requests;
using Mapster;

namespace Instagram.Api.Mappings;

public class UserMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>();
    }
}