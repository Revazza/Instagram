using FluentValidation;
using Instagram.Application.Common.Extensions.BuiltInTypes;

namespace Instagram.Application.Common.Extensions.FluentValidator;

public static class StringFluentValidator
{

    public static IRuleBuilderOptions<T, string> MustNotContainNumbers<T>(
        this IRuleBuilderOptions<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(s => !s.ContainsNumber()).WithMessage("'{PropertyName}' must not contain number");
    }


}