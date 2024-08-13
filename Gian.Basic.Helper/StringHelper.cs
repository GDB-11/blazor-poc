using System.Text.RegularExpressions;

namespace Gian.Basic.Helper;

public static partial class StringHelper
{
    public static string ReplaceSpecialCharacters(this string text, string replacement = "")
    {
        return SafeCharactersRegex().Replace(text, replacement);
    }

    [GeneratedRegex(@"[^a-zA-Z0-9\s]")]
    private static partial Regex SafeCharactersRegex();
}
