using System.Text.RegularExpressions;

class JackTokenizer
{
    private StreamReader fileStream;
    private IToken? nextToken;

    public JackTokenizer(string inputFilePath)
    {
        fileStream = new StreamReader(inputFilePath);
        InitTokenizer();
    }

    public bool HasMoreTokens()
    {
        return nextToken != null;
    }

    public IToken Advance()
    {
        if (nextToken == null)
        {
            throw new Exception("No token to read.");
        }

        IToken returnToken = nextToken;

        ReadNextToken();

        return returnToken;
    }

    /*
     * PRIVATE METHODS
     */

    private void InitTokenizer()
    {
        ReadNextToken();
    }

    private void ReadNextToken()
    {
        IToken? peekToken = null;

        SkipCommentsAndEOL();

        string currentWord = "";

        // Read until the "currentWord" matches a known token.
        while (fileStream.Peek() > 0)
        {

            char nextChar = (char)fileStream.Read();
            currentWord += nextChar;

            peekToken = CheckWordForToken(currentWord);

            if (peekToken != null)
            {
                break;
            }
        }

        nextToken = peekToken;
    }

    /** This method will read through Comments and EOL. It returns true if it found something, false otherwise.
      * Then it can be used in a loop to ignore recursively Comments and EOL.
      */
    private void SkipCommentsAndEOL()
    {
        bool skipped = false;

        do
        {
            skipped = false;
            // All comments lines start by a "/" (//, /* or /**)
            char peekChar = (char)fileStream.Peek();
            if (peekChar == '/')
            {
                fileStream.Read();
                char secondCommentChar = (char)fileStream.Read();

                // If it's a "//" comment, read until the EOL
                if (secondCommentChar == '/')
                {
                    char nextChar;
                    do
                    {
                        nextChar = (char)fileStream.Read();
                    } while (nextChar != '\n');

                    skipped = true;
                }
                // If it's a /* comment, read until "* /"
                else if (secondCommentChar == '*')
                {
                    bool reachedEndOfComment = false;

                    char nextChar;
                    do
                    {
                        nextChar = (char)fileStream.Read();
                        if (nextChar == '*')
                        {
                            peekChar = (char)fileStream.Peek();
                            if (peekChar == '/')
                            {
                                reachedEndOfComment = true;
                            }
                        }
                    } while (reachedEndOfComment == false);

                    skipped = true;
                }
            }
            else if (peekChar == '\n')
            {
                fileStream.Read();

                skipped = true;
            }
        } while (skipped == true);
    }

    private IToken? CheckWordForToken(string word)
    {
        KeywordToken? keywordToken = CheckKeywordToken(word);

        if (keywordToken != null)
        {
            return keywordToken;
        }

        SymbolToken? symbolToken = CheckSymbolToken(word);

        return null;

    }

    private KeywordToken? CheckKeywordToken(string word)
    {
        bool matches = Enum.TryParse<KeywordType>(word, true, out _);

        if (matches)
        {
            return new KeywordToken(word);
        }

        return null;
    }

    private SymbolToken? CheckSymbolToken(string word)
    {
        SymbolTypeEnum matches;
        SymbolType.TryParse(word, out matches);

        return null;
    }

}