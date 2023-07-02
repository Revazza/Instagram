using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Application.Users.Commands.CreateUser;

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
        var userWithEmail = await _userRepository.FindByEmail(command.Email);

        if (userWithEmail is not null)
        {
            return new Response().IsFailure($"User with {command.Email} already exists");
        }

        var user = _mapper.Map<CreateUserCommand, User>(command);
        

        await _userManager.AddPasswordAsync(user, command.Password);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var newUser = _mapper.Map<User, CreateUserResponse>(user);
        return new Response()
            .Add("newUser", newUser);
    }
}