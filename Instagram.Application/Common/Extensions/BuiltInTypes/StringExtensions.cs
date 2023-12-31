using Microsoft.IdentityModel.Tokens;

namespace Instagram.Application.Common.Extensions.BuiltInTypes;

public static class StringExtensions
{

    public static bool ContainsNumber(this string str)
    {
        return str.Any(char.IsDigit);
    }

    public static int ToInt(this string str)
    {
        if (int.TryParse(str, out int result))
        {
            return result;
        }
        return -1;
    }

    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }


}