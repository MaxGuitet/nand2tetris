public class SymbolTypeDict
{
    private static Dictionary<char, SymbolTypeEnum> SymbolsToEnum = new Dictionary<char, SymbolTypeEnum>
    {
        ['{'] = SymbolTypeEnum.CURLY_BRACKETS_OPEN,
        ['}'] = SymbolTypeEnum.CURLY_BRACKETS_CLOSE,
        ['('] = SymbolTypeEnum.PAREN_OPEN,
        [')'] = SymbolTypeEnum.PAREN_CLOSE,
        ['['] = SymbolTypeEnum.SQUARE_BRACKETS_OPEN,
        [']'] = SymbolTypeEnum.SQUARE_BRACKETS_CLOSE,
        ['.'] = SymbolTypeEnum.DOT,
        [','] = SymbolTypeEnum.COMMA,
        [';'] = SymbolTypeEnum.SEMI_COLON,
        ['+'] = SymbolTypeEnum.PLUS,
        ['-'] = SymbolTypeEnum.MINUS,
        ['*'] = SymbolTypeEnum.TIMES,
        ['/'] = SymbolTypeEnum.DIVIDE,
        ['&'] = SymbolTypeEnum.AMP,
        ['|'] = SymbolTypeEnum.PIPE,
        ['<'] = SymbolTypeEnum.LT,
        ['>'] = SymbolTypeEnum.GT,
        ['='] = SymbolTypeEnum.EQUAL,
        ['~'] = SymbolTypeEnum.NOT
    };


    private static Dictionary<SymbolTypeEnum, char> EnumToSymbols = new Dictionary<SymbolTypeEnum, char>
    {
        [SymbolTypeEnum.CURLY_BRACKETS_OPEN] = '{',
        [SymbolTypeEnum.CURLY_BRACKETS_CLOSE] = '}',
        [SymbolTypeEnum.PAREN_OPEN] = '(',
        [SymbolTypeEnum.PAREN_CLOSE] = ')',
        [SymbolTypeEnum.SQUARE_BRACKETS_OPEN] = '[',
        [SymbolTypeEnum.SQUARE_BRACKETS_CLOSE] = ']',
        [SymbolTypeEnum.DOT] = '.',
        [SymbolTypeEnum.COMMA] = ',',
        [SymbolTypeEnum.SEMI_COLON] = ';',
        [SymbolTypeEnum.PLUS] = '+',
        [SymbolTypeEnum.MINUS] = '-',
        [SymbolTypeEnum.TIMES] = '*',
        [SymbolTypeEnum.DIVIDE] = '/',
        [SymbolTypeEnum.AMP] = '&',
        [SymbolTypeEnum.PIPE] = '|',
        [SymbolTypeEnum.LT] = '<',
        [SymbolTypeEnum.GT] = '>',
        [SymbolTypeEnum.EQUAL] = '=',
        [SymbolTypeEnum.NOT] = '~'
    };

    public static char GetSymbolFromEnum(SymbolTypeEnum symbol)
    {
        return EnumToSymbols.GetValueOrDefault(symbol);
    }
};

