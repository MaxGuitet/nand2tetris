class ReturnCommand : ICommand
{
    public override string GetAsmCode()
    {
        string[] words = {
            "// return",
            SetEndFrameToLCL(),
            RestorePointer("return", "R14", "5"), // save return address in R14
            PutReturnValueInArg(),
            RepositionSP(),
            RestorePointer("that", "THAT", "1"),
            RestorePointer("this", "THIS", "2"),
            RestorePointer("arg", "ARG", "3"),
            RestorePointer("local", "LCL", "4"),
            GotoReturnAddr()
        };

        return JoinString(words);
    }

    private string SetEndFrameToLCL()
    {
        string[] words =  {
            "// set endFrame to LCL",
            "@LCL",
            "D=M",
            "@R13",
            "M=D"
        };

        return JoinString(words);
    }

    private string PutReturnValueInArg()
    {
        string[] words = {
            "// restore ARG",
            "@SP",
            "A=M-1",
            "D=M",
            "@ARG",
            "A=M",
            "M=D"
        };

        return JoinString(words);
    }

    private string RepositionSP()
    {
        string[] words = {
            "// reposition SP",
            "@ARG",
            "D=M+1",
            "@SP",
            "M=D"
        };

        return JoinString(words);
    }

    private string RestorePointer(string targetName, string targetLabel, string offset)
    {
        string[] words = {
            $"// restore {targetName}",
            "@R13",
            "D=M",
            $"@{offset}",
            "A=D-A",
            "D=M",
            $"@{targetLabel}",
            "M=D"
        };

        return JoinString(words);
    }

    private string GotoReturnAddr()
    {
        string[] words = {
            "// go to return address",
            "@R14",
            "A=M",
        };

        return JoinString(words);
    }
}