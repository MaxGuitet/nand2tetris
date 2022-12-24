public abstract class Command
{
    internal CommandType type;

    public abstract string GetAsmCode();
}