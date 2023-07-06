using FluentValidation;

namespace Instagram.Application.Users.Queries.FilterUsersByUserName;

public class FilterUsersByUserNameValidator : AbstractValidator<FilterUsersByUserNameQuery>
{

	public FilterUsersByUserNameValidator()
	{
		RuleFor(x =>x.UserName)
			.NotEmpty()
			.MaximumLength(50);

	}

}