class IfgotoCommand : ICommand
{
    private string label;

    internal IfgotoCommand(string label)
    {
        type = CommandType.C_GOTO;
        this.label = label;
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