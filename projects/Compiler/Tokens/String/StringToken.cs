using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public class StringToken : IToken
{
    private string value;

    internal override TokenType type => TokenType.STRING_CONST;

    public StringToken(string word)
    {
        // Remove starting and ending `"`
        value = word.Substring(1, word.Length - 2);
    }

    public override string GetXMLCode()
    {
        return $"<string>{value}</string>";
    }

    public static bool TryParse(string word, [MaybeNullWhen(false)] out StringToken token)
    {
        token = null;

        if (Regex.IsMatch(word, "^\"[^\"]\"$"))
        {
            token = new StringToken(word);
            return true;
        }

        return false;
    }
}