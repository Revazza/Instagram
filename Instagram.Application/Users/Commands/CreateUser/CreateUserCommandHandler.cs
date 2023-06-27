using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using Instagram.Domain.Chats;
using Instagram.Domain.Users;
using Instagram.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile(
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            Age: request.Age);

        var user = new User()
        {
            UserId = new UserId(Guid.NewGuid()),
            Profile = userProfile,
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new Response()
            .Add("newUser", user);
    }
}