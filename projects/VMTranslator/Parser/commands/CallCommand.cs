class CallCommand : ICommand
{
    private string incrSP = "@SP\nM=M+1";
    private string pushDToSP = "@SP\nA=M\nM=D";
    private static int index = -1;
    private string functionName;
    private int nArgs;
    private string fileName;

    public CallCommand(string functionName, int nArgs, string fileName)
    {
        this.functionName = functionName;
        this.nArgs = nArgs;
        this.fileName = fileName;

    }
    public override string GetAsmCode()
    {
        string retAddrLabel = GetRetAddrLabel();
        string[] words = {
            $"// call {functionName} {nArgs}",
            PushRetAddrLabel(retAddrLabel),
            PushPointerAddress("LCL"),
            PushPointerAddress("ARG"),
            PushPointerAddress("THIS"),
            PushPointerAddress("THAT"),
            SetArgPosition(),
            SetNewLCL(),
            GotoFunction(),
            AddRetLabel(retAddrLabel),
        };

        return JoinString(words);
    }

    private string GetRetAddrLabel()
    {
        index += 1;
        return LabelUtils.GetLabel($"ret{index}", fileName, functionName);
    }

    private string PushRetAddrLabel(string label)
    {
        string[] words = {
            $"// push return address for @{label}",
            $"@{label}",
            "D=A",
            pushDToSP,
            incrSP
        };

        return JoinString(words);
    }

    private string PushPointerAddress(string pointer)
    {
        string[] words ={
            $"// push {pointer}",
            $"@{pointer}",
            "D=M",
            pushDToSP,
            incrSP
};
        return JoinString(words);
    }

    private string SetArgPosition()
    {
        List<string> words = new List<string>()
        {
            "// Set ARG position",
            "@SP",
            "D=M",
            "@5",
            "D=D-A",
        };

        for (int i = 0; i < nArgs; i++)
        {
            words.Add("D=D-1");
        }

        words.Add("@ARG");
        words.Add("M=D");

        return JoinString(words.ToArray<string>());
    }

    private string SetNewLCL()
    {
        string[] words = {
            "// Set new LCL value",
            "@SP",
            "D=M",
            "@LCL",
            "M=D"
        };

        return JoinString(words);
    }

    private string GotoFunction()
    {
        GotoCommand command = new GotoCommand(functionName);
        return command.GetAsmCode();
    }

    private string AddRetLabel(string label)
    {
        LabelCommand command = new LabelCommand(label);

        return command.GetAsmCode();
    }
}