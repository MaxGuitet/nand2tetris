public abstract class ICommand
{
    internal CommandType type;

    public abstract string GetAsmCode();
}