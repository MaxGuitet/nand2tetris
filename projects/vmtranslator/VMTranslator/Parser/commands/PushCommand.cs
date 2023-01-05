internal class PushCommand : ICommand
{
    string incrSP = "@SP\nM=M+1";
    string fileName;
    string[] parts;
    internal PushCommand(string[] commandParts, string fileName)
    {
        type = CommandType.C_PUSH;
        parts = commandParts;
        this.fileName = fileName;
    }

    public override string GetAsmCode()
    {
        string segment = parts[1];
        string element = parts[2];

        switch (segment)
        {
            case "local":
                return GetGeneric(segment, "LCL", element);

            case "argument":
                return GetGeneric(segment, "ARG", element);

            case "this":
                return GetGeneric(segment, "THIS", element);

            case "that":
                return GetGeneric(segment, "THAT", element);

            case "constant":
                return GetConstant();

            case "static":
                {
                    string pointerName = $"{fileName}.{element}";
                    return GetDirectMemoryValue(segment, pointerName, element);
                }

            case "pointer":
                {
                    if (element != "0" && element != "1")
                    {
                        throw new InvalidCommandException($"Invalid pointer segment value \"{element}\".");
                    }
                    string pointerName = "";

                    if (element == "0")
                    {
                        pointerName = "THIS";
                    }
                    else
                    {
                        pointerName = "THAT";
                    }
                    return GetDirectMemoryValue(segment, pointerName, element);
                }

            case "temp":
                // temp variables are stored in RAM[5+i], with exactly 8 slots available
                // throw if ement >= 8
                int elementNumber = int.Parse(element);

                if (elementNumber >= 8)
                {
                    throw new InvalidCommandException("temp segment overflow.");
                }

                return GetTemp(element);

            default:
                throw new Exception($"Unknown segment for push instruction \"{segment}\".");
        }
    }

    string GetGeneric(string segmentName, string pointerName, string element)
    {
        string[] words = {
            $"// push {segmentName} {element}",
            $"@{pointerName}",
            "D=M",
            $"@{element}",
            "D=D+A",
            // Go to address POINTER + i
            "A=D",
            // store value to push
            "D=M",
            // push to stack
            "@SP",
            "A=M",
            "M=D",
            incrSP

        };
        return JoinString(words);
    }

    // GetTemp is almost the same than GetGeneric, but the second instruction
    // is D=A instead of D=M. 
    // Indeed, we know that the TEMP segment starts at 5 but we don't have symbol for it
    string GetTemp(string element)
    {
        string[] words = {
            $"// push temp {element}",
            $"@5",
            "D=A",
            $"@{element}",
            "D=D+A",
            // Go to address POINTER + i
            "A=D",
            // store value to push
            "D=M",
            // push to stack
            "@SP",
            "A=M",
            "M=D",
            incrSP

        };
        return JoinString(words);
    }

    string GetDirectMemoryValue(string segmentName, string pointerName, string element)
    {

        string[] words = {
            $"// push {segmentName} {element}",
            $"@{pointerName}",
            // store value to push
            "D=M",
            // push to stack
            "@SP",
            "A=M",
            "M=D",
            incrSP

        };
        return JoinString(words);
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
            incrSP
        };

        return JoinString(words);
    }
}