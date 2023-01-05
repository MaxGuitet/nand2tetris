class IfgotoCommand : ICommand
{
    string label;

    internal IfgotoCommand(string[] commandParts)
    {
        type = CommandType.C_GOTO;
        label = commandParts[1];
    }

    public override string GetAsmCode()
    {
        string[] words = {
            "@SP",
            "M=M-1",
            "A=M",
            "D=M",
            // Jump if the value is not false, ie not 0
            $"@{label}",
            "D;JNE"
        };

        return JoinString(words);
    }
}