using FluentValidation;
using Instagram.Application.Common.Extensions.FluentValidator;

namespace Instagram.Application.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(2, 255)
            .MustBeEnglish();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .Length(2, 50)
            .MustNotContainNumbers()
            .MustBeEnglish();

    }
}