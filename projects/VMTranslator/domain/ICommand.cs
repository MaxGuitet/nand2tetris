public abstract class ICommand
{
    internal CommandType type;

    public abstract string GetAsmCode();

    internal string JoinString(params string[] words)
    {
        return string.Join("\n", words);
    }
}