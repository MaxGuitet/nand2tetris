using System.Diagnostics.CodeAnalysis;

public class SymbolToken : IToken
{
    SymbolTypeEnum symbolType;

    internal override TokenType type => TokenType.SYMBOL;

    public SymbolToken(SymbolTypeEnum symbolType)
    {
        this.symbolType = symbolType;
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

        SymbolTypeEnum? symbolType = SymbolTypeDict.GetEnumFromSymbol(symbol[0]);
        if (symbolType != null)
        {
            outVar = new SymbolToken((SymbolTypeEnum)symbolType);
            return true;
        }

        return false;
    }
}
