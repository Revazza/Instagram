using FluentValidation;

namespace Instagram.Application.Queries.Users.FilterUsersByUserName;

public class FilterUsersByUserNameValidator : AbstractValidator<FilterUsersByUserNameQuery>
{

    public FilterUsersByUserNameValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

    }

}