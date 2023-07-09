using FluentValidation;

namespace Instagram.Application.DomainEntities.Users.Queries.FilterUsersByUserName;

public class FilterUsersByUserNameValidator : AbstractValidator<FilterUsersByUserNameQuery>
{

    public FilterUsersByUserNameValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

    }

}