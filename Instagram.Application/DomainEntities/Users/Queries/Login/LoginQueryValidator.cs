using FluentValidation;

namespace Instagram.Application.DomainEntities.Users.Queries.Login;

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