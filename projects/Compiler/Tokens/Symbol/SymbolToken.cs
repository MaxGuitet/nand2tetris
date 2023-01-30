class SymbolToken : IToken
{
    SymbolTypeEnum symbolType;
    public SymbolToken(string symbol)
    {
        if (symbol.Length > 1)
        {
            throw new JackSyntaxException($"Symbols can only be one letter long.");
        }
        try
        {
            SymbolType.TryParse(symbol, out symbolType);
        }
        catch (Exception ex)
        {
            throw new JackSyntaxException($"Unknown Symbol \"{symbol}\".", ex);
        }
    }

    public override string GetXMLCode()
    {
        return $"<keyword>{SymbolType.GetSymbolFromEnum(symbolType)}</keyword>";
    }
}