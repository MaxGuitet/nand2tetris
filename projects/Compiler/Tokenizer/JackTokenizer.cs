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

        string currentWord = "";
        IToken? peekToken = null;
        bool fullWord = false;

        SkipWhiteSpaces();
        SkipCommentsAndEOL();


        // Read until the "currentWord" matches a known token.
        while (fileStream.Peek() > 0)
        {
            char nextChar = (char)fileStream.Read();

            // When reaching a white space, we have finish the word. We can try to parse it as Int or identifier.
            if (nextChar == ' ')
            {
                fullWord = true;
            }

            currentWord += nextChar;

            peekToken = CheckWordForToken(currentWord, fullWord);

            if (peekToken != null)
            {
                break;
            }

            // If nextChar is a white space, we've reached the end of the word but no match was found.
            // throw Exception.
            if (nextChar == ' ')
            {
                throw new JackSyntaxException($"Unknow token {currentWord}");
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

    /**
     * This method will simply skip white spaces between tokens.
     */
    private void SkipWhiteSpaces()
    {
        while ((char)fileStream.Peek() == ' ')
        {
            fileStream.Read();
        }
    }

    private IToken? CheckWordForToken(string word, bool fullWord)
    {
        KeywordToken? keywordToken = CheckKeywordToken(word);

        if (keywordToken != null)
        {
            return keywordToken;
        }

        SymbolToken? symbolToken = CheckSymbolToken(word);

        if (symbolToken != null)
        {
            return symbolToken;
        }

        // Can only parse int and identifiers if full words
        if (fullWord)
        {
            IntegerToken? intToken = CheckIntegerToken(word);

            if (intToken != null)
            {
                return intToken;
            }
        }

        return null;

    }

    private KeywordToken? CheckKeywordToken(string word)
    {
        bool match = Enum.TryParse<KeywordTypeEnum>(word, true, out _);

        if (match)
        {
            return new KeywordToken(word);
        }

        return null;
    }

    private SymbolToken? CheckSymbolToken(string word)
    {
        SymbolToken match;
        if (SymbolToken.TryParse(word, out match))
        {
            return match;
        }
        return null;

    }

    private IntegerToken? CheckIntegerToken(string word)
    {
        IntegerToken match;
        if (IntegerToken.TryParse(word, out match))
        {
            return match;
        }

        return null;
    }

}