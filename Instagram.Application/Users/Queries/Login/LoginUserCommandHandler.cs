using Instagram.Application.Common;
using MediatR;

namespace Instagram.Application.Users.Queries.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response>
{
    public Task<Response> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}