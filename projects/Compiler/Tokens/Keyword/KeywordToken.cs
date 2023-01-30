class KeywordToken : IToken
{
    KeywordType keywordType;
    public KeywordToken(string keyword)
    {
        try
        {
            keywordType = Enum.Parse<KeywordType>(keyword.ToUpper());
        }
        catch (Exception ex)
        {
            throw new JackSyntaxException($"Unknown keyword \"{keyword}\".", ex);
        }
    }

    public override string GetXMLCode()
    {
        return $"<keyword>{Enum.GetName<KeywordType>(keywordType).ToLower()}</keyword>";
    }
}