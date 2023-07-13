using Instagram.Application.Commands.Users.CreateUser;
using Instagram.Contracts.Users.Requests;
using Mapster;

namespace Instagram.Api.Mappings;

public class UserMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>();
        
    }
}