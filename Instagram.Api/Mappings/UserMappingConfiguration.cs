using Instagram.Application.Users.Commands.CreateUser;
using Instagram.Application.Users.Queries.FilterUsersByUserName;
using Instagram.Contracts.User.Requests;
using Mapster;
using Newtonsoft.Json;

namespace Instagram.Api.Mappings;

public class UserMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>();
        
    }
}