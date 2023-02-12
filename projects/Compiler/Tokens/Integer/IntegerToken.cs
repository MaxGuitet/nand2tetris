using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public class IntegerToken : IToken
{
    private int value;
    public IntegerToken(string word)
    {
        value = Int16.Parse(word);

        if (value < 0)
        {
            throw new JackSyntaxException("Only positive integers are allowed.");
        }
    }

    public int Value { get => value; }

    internal override TokenType type => TokenType.INT_CONST;

    public override string GetXMLCode()
    {
        return $"<constant>{value}</constant>";
    }

    public static bool TryParse(string word, [MaybeNullWhen(false)] out IntegerToken token)
    {
        token = null;

        if (!Regex.IsMatch(word, "^\\d+$"))
        {
            return false;
        }

        short intValue;
        bool isValid = Int16.TryParse(word, out intValue);

        if (isValid && intValue >= 0)
        {
            token = new IntegerToken(word);
        }

        return isValid;
    }
}