using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Application.Commands.Users.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IMapper mapper,
        UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {

        var userWithEmail = await _userManager.FindByEmailAsync(command.Email);

        if (userWithEmail is not null)
        {
            return Response.Error($"User with {command.Email} already exists");
        }

        var newUser = _mapper.Map<CreateUserCommand, User>(command);

        var userCreated = await _userManager.CreateAsync(newUser, command.Password);

        if (!userCreated.Succeeded)
        {
            return Response.Error(userCreated.Errors.First().Description);
        }
        await _userRepository.SaveChangesAsync();

        var userResponse = _mapper.Map<User, CreateUserResponse>(newUser);
        return Response
            .Ok()
            .Add("newUser", userResponse);
    }



}