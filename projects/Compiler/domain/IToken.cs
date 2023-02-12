public abstract class IToken
{
    internal abstract TokenType type { get; }

    public abstract string GetXMLCode();
}