abstract class Command
{
    protected CommandType type;

    public abstract string getAsmCode();
}