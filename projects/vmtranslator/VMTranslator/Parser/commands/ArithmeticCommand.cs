class ArithmeticCommand : ICommand
{
    static int jumpIndex = -1;
    string incrSP = "@SP\nM=M+1";
    string decrSP = "@SP\nM=M-1";

    string operation;

    internal ArithmeticCommand(string[] commandParts)
    {
        type = CommandType.C_ARITHMETIC;
        operation = commandParts[0];
    }
    public override string GetAsmCode()
    {
        switch (operation)
        {
            case "add":
                return GetTwoFactorsOp("add", "+");

            case "sub":
                return GetTwoFactorsOp("sub", "-");

            case "neg":
                return GetNeg();

            case "eq":
                return GetComparison("eq", "-", "JEQ");

            case "gt":
                return GetComparison("gt", "-", "JGT");

            case "lt":
                return GetComparison("lt", "-", "JLT");

            case "and":
                return GetTwoFactorsOp("and", "&");

            case "or":
                return GetTwoFactorsOp("or", "|");

            case "not":
                return GetNot();

            default:
                throw new InvalidCommandException($"Unknown arithmetic operation \"operation\"");
        }
    }

    string GetTwoFactorsOp(string opType, string opString)
    {
        return JoinString(
            $"// {opType}",
            decrSP,
            "A=M",
            "D=M",
            decrSP,
            "A=M",
            $"D=M{opString}D",
            "@SP",
            "A=M",
            "M=D",
            incrSP
        );
    }

    string GetNeg()
    {
        return JoinString(
            "// neg",
            decrSP,
            "A=M",
            "D=-M",
            "M=D",
            incrSP
        );
    }

    string GetComparison(string opType, string opSymbol, string jumpCondition)
    {
        jumpIndex += 1;
        return JoinString(
            $"// {opType}",
            decrSP,
            "A=M",
            "D=M",
            decrSP,
            "A=M",
            $"D=M{opSymbol}D",
            $"@TRUE{jumpIndex}",
            $"D;{jumpCondition}",
            $"@FALSE{jumpIndex}",
            "0;JMP",
            $"(TRUE{jumpIndex})",
            "  D=-1",
            $"  @END{jumpIndex}",
            "  0;JMP",
            $"(FALSE{jumpIndex})",
            "  D=0",
            $"(END{jumpIndex})",
            "  @SP",
            "  A=M",
            "  M=D",
            incrSP
        );
    }

    string GetNot()
    {
        jumpIndex += 1;
        return JoinString(
            $"// not",
            decrSP,
            "A=M",
            "D=!M",
            // "D=D+1", // In the end, D = -x + 1; if x was false (0) => -0 + 1 = 1, if x as true (1) => -1 + 1 = 0; 
            "@SP",
            "A=M",
            "M=D",
            incrSP
        );
    }
}