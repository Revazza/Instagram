using Instagram.Application.Authentication;
using Instagram.Application.Common;
using Instagram.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Application.Queries.Users.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _tokenGenerator;


    public LoginQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Response> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Response.Error("Incorrect credentials");
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordCorrect)
        {
            return Response.Error("Incorrect Credentials");
        }

        var token = _tokenGenerator.Generate(user);

        return Response.Ok().Add("token", token);
    }
}