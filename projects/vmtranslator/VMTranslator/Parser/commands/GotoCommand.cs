internal class GotoCommand : ICommand
{
    string label;

    internal GotoCommand(string[] commandParts)
    {
        type = CommandType.C_GOTO;
        label = commandParts[1];
    }

    public override string GetAsmCode()
    {
        return $"@{label}\n0;JMP";
    }
}