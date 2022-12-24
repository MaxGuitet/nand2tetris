internal class PushCommand : Command
{
    string[] parts;
    internal PushCommand(string[] commandParts)
    {
        type = CommandType.C_PUSH;
        parts = commandParts;
    }

    public override string GetAsmCode()
    {
        string segment = parts[1];
        string element = parts[2];

        switch (segment)
        {
            case "local":
                return GetLocal();

            case "argument":
                return "";

            case "this":
                return "";

            case "that":
                return "";

            case "constant":
                return GetConstant();

            case "static":
                return "";

            case "pointer":
                return "";

            case "temp":
                return "";
            default:
                throw new Exception($"Unknown segment for push instruction \"{segment}\"");
        }
    }

    string GetLocal()
    {
        return "";
    }

    string GetArgument()
    {
        return "";
    }

    string GetThis()
    {
        return "";
    }

    string GetThat()
    {
        return "";
    }

    string GetConstant()
    {
        string element = parts[2];
        string[] words = {
            $"// push constant {element}",
            $"@{element}",
            "D=A",
            "@SP",
            "A=M",
            "M=D",
            "@SP",
            "M=M+1"
        };

        return string.Join("\n", words);
    }

    string GetStatic()
    {
        return "";
    }

    string GetPointer()
    {
        return "";
    }

    string GetTemp()
    {
        return "";
    }
}