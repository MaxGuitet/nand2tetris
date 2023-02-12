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

        SkipCommentsWhiteSpacesAndEOL();


        // Read until the "currentWord" matches a known token.
        while (fileStream.Peek() > 0)
        {
            char peekChar = (char)fileStream.Peek();

            if (peekChar == '"')
            {
                peekToken = ParseStringToken();
                break;
            }

            // First, check if next char is a symbol. In which case we need to stop (eg when having a function "main()")
            SymbolToken? peekCharAsSymbol = CheckSymbolToken(peekChar.ToString());

            // When reaching a symbol or white space, stop reading
            if (currentWord.Length > 0 && (peekCharAsSymbol != null || peekChar == ' ' || peekChar == '\n'))
            {
                fullWord = true;
            }
            else
            {
                char nextChar = (char)fileStream.Read();
                currentWord += nextChar;
            }


            if (currentWord.Length > 0)
            {
                peekToken = CheckWordForToken(currentWord, fullWord);
            }
            else
            {
                continue;
            }

            if (peekToken != null)
            {
                break;
            }

            // If we've reached the end of the word but no match was found.
            // throw Exception.
            if (fullWord == true)
            {
                throw new JackSyntaxException($"Unknow token {currentWord}");
            }
        }

        nextToken = peekToken;
    }

    /** This method will read through Comments and EOL. It returns true if it found something, false otherwise.
     * Then it can be used in a loop to ignore recursively Comments and EOL.
     */
    private void SkipCommentsWhiteSpacesAndEOL()
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
            else if (peekChar == '\n' || peekChar == ' ')
            {
                fileStream.Read();

                skipped = true;
            }
        } while (skipped == true);
    }

    private IToken? CheckWordForToken(string word, bool fullWord)
    {

        SymbolToken? symbolToken = CheckSymbolToken(word);

        if (symbolToken != null)
        {
            return symbolToken;
        }

        if (fullWord)
        {

            // Need to check Integer before Keyword otherwise Int are parsed as enum value
            IntegerToken? intToken = CheckIntegerToken(word);

            if (intToken != null)
            {
                return intToken;
            }

            KeywordToken? keywordToken = CheckKeywordToken(word);

            if (keywordToken != null)
            {
                return keywordToken;
            }

            IdentifierToken? identifierToken = CheckIdentifierToken(word);

            if (identifierToken != null)
            {
                return identifierToken;
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
        SymbolToken? match;
        if (SymbolToken.TryParse(word, out match))
        {
            return match;
        }
        return null;

    }

    private StringToken? ParseStringToken()
    {
        string fullString = "";
        fullString += (char)fileStream.Read();

        while ((char)fileStream.Peek() != '"')
        {
            fullString += (char)fileStream.Read();
        }

        // When we go out of the loop, we still have the last `"` to read.
        fullString += (char)fileStream.Read();

        StringToken match = new StringToken(fullString);

        return match;
    }

    private IntegerToken? CheckIntegerToken(string word)
    {
        IntegerToken? match;
        if (IntegerToken.TryParse(word, out match))
        {
            return match;
        }

        return null;
    }

    private IdentifierToken? CheckIdentifierToken(string word)
    {
        IdentifierToken? match;
        if (IdentifierToken.TryParse(word, out match))
        {
            return match;
        }

        return null;
    }

}