internal class LabelCommand : ICommand
{
    string label;

    internal LabelCommand(string[] commandParts)
    {
        type = CommandType.C_GOTO;
        label = commandParts[1];
    }

    public override string GetAsmCode()
    {
        return $"({label})";
    }
}