using FluentValidation;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using System.Text.RegularExpressions;

namespace Instagram.Application.Common.Extensions.FluentValidator;

public static class StringFluentValidator
{

    public static IRuleBuilderOptions<T, string> MustNotContainNumbers<T>(
        this IRuleBuilderOptions<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(s => !s.ContainsNumber()).WithMessage("'{PropertyName}' must not contain number");
    }

    public static IRuleBuilderOptions<T, string> MustBeEnglish<T>(
        this IRuleBuilderOptions<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(s => Regex.IsMatch(s.Replace(" ", ""), "^[a-zA-Z0-9]*$")).WithMessage("'{PropertyName}' must be English");
    }


}