internal class GotoCommand : ICommand
{
    private string label;

    internal GotoCommand(string label)
    {
        type = CommandType.C_GOTO;
        this.label = label;
    }

    public override string GetAsmCode()
    {
        return $"@{label}\n0;JMP";
    }
}