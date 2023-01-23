internal class LabelCommand : ICommand
{
    private string label;

    internal LabelCommand(string label)
    {
        type = CommandType.C_GOTO;
        this.label = label;
    }

    public override string GetAsmCode()
    {
        return $"({label})";
    }
}