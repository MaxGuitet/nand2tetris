using System.Diagnostics.CodeAnalysis;

public class SymbolToken : IToken
{
    SymbolTypeEnum symbolType;
    public SymbolToken(char symbol)
    {
        if (!SymbolTypeEnum.TryParse(symbol.ToString(), out symbolType))
        {

            throw new JackSyntaxException($"Unknown Symbol \"{symbol}\".");
        }
    }

    public override string GetXMLCode()
    {
        return $"<keyword>{SymbolTypeDict.GetSymbolFromEnum(symbolType)}</keyword>";
    }

    public static bool TryParse(string symbol, [MaybeNullWhen(false)] out SymbolToken outVar)
    {
        outVar = null;

        if (symbol.Length > 1)
        {
            return false;
        }

        if (Enum.TryParse<SymbolTypeEnum>(symbol, true, out _))
        {
            outVar = new SymbolToken(symbol[0]);
            return true;
        }

        return false;
    }
}