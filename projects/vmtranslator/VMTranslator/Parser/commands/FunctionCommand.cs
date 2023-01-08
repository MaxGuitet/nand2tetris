class FunctionCommand : ICommand
{
    private string incrSP = "@SP\nM=M+1";
    private string functionName;
    private int nLocals;

    public FunctionCommand(string functionName, int nLocals)
    {
        this.functionName = functionName;
        this.nLocals = nLocals;
    }

    public override string GetAsmCode()
    {
        string[] words = {
            $"// function {functionName} {nLocals}",
            $"({functionName})",
            RepeatPushZero(nLocals),
        };

        return JoinString(words);
    }

    private string PushZero(int varNumber = 0)
    {
        PushCommand push0 = new PushCommand("constant", "0");
        PopCommand popLocal = new PopCommand("local", $"{varNumber}");

        return push0.GetAsmCode() + "\n" + popLocal.GetAsmCode() + "\n" + incrSP;
    }

    private string RepeatPushZero(int n)
    {
        string[] words = new string[n];

        for (int i = 0; i < n; i++)
        {
            words[i] = PushZero(i);
        }

        return JoinString(words);
    }
}