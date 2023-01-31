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

    public override string GetXMLCode()
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string word, [MaybeNullWhen(false)] out IntegerToken token)
    {
        token = null;

        if (!Regex.IsMatch(word, "^\\d+$"))
        {
            return false;
        }

        bool isValid = Int32.TryParse(word, out _);
        if (isValid)
        {
            token = new IntegerToken(word);
        }

        return isValid;
    }
}