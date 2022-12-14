class ArithmeticCommand : ICommand
{
    static int jumpIndex = -1;
    string incrSP = "@SP\nM=M+1";
    string decrSP = "@SP\nM=M-1";

    ArithmeticOperation operation;

    internal ArithmeticCommand(ArithmeticOperation operation)
    {
        type = CommandType.C_ARITHMETIC;
        this.operation = operation;
    }
    public override string GetAsmCode()
    {
        switch (operation)
        {
            case ArithmeticOperation.add:
                return GetTwoFactorsOp("add", "+");

            case ArithmeticOperation.sub:
                return GetTwoFactorsOp("sub", "-");

            case ArithmeticOperation.neg:
                return GetNeg();

            case ArithmeticOperation.eq:
                return GetComparison("eq", "-", "JEQ");

            case ArithmeticOperation.gt:
                return GetComparison("gt", "-", "JGT");

            case ArithmeticOperation.lt:
                return GetComparison("lt", "-", "JLT");

            case ArithmeticOperation.and:
                return GetTwoFactorsOp("and", "&");

            case ArithmeticOperation.or:
                return GetTwoFactorsOp("or", "|");

            case ArithmeticOperation.not:
                return GetNot();

            default:
                throw new InvalidCommandException($"Unknown arithmetic operation \"{operation}\"");
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