class KeywordToken : IToken
{
    KeywordTypeEnum keywordType;
    public KeywordToken(string keyword)
    {
        try
        {
            keywordType = Enum.Parse<KeywordTypeEnum>(keyword.ToUpper());
        }
        catch (Exception ex)
        {
            throw new JackSyntaxException($"Unknown keyword \"{keyword}\".", ex);
        }
    }

    public override string GetXMLCode()
    {
        return $"<keyword>{Enum.GetName<KeywordTypeEnum>(keywordType).ToLower()}</keyword>";
    }
}