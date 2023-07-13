using FluentValidation;

namespace Instagram.Application.Queries.Users.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{

    public LoginQueryValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();

    }

}