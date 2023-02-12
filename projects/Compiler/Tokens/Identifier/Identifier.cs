using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public class IdentifierToken : IToken
{
    static string regex = "[a-zA-Z_][a-zA-Z0-9_]*";
    string value;

    internal override TokenType type => TokenType.IDENTIFIER;

    public static bool TestRegex(string testString)
    {
        return Regex.IsMatch(testString, regex);
    }

    public static bool TryParse(string word, [MaybeNullWhen(false)] out IdentifierToken token)
    {
        token = null;

        if (!TestRegex(word))
        {
            return false;
        }


        token = new IdentifierToken(word);
        return true;
    }

    public IdentifierToken(string word)
    {
        value = word;
    }


    public override string GetXMLCode()
    {
        return $"<varName>{value}</varName>";
    }
}