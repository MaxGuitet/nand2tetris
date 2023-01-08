class FunctionCommand : ICommand
{
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

    private string PushZero()
    {
        PushCommand push = new PushCommand("local", "0");

        return push.GetAsmCode();
    }

    private string RepeatPushZero(int n)
    {
        string[] words = new string[n];

        for (int i = 0; i < n; i++)
        {
            words[i] = PushZero();
        }

        return JoinString(words);
    }
}