internal class PushCommand : ICommand
{
    private string incrSP = "@SP\nM=M+1";
    private string pushToSP = "@SP\nA=M\nM=D";
    private string fileName;
    private string segment;
    private string element;

    internal PushCommand(string segment, string element, string fileName)
    {
        type = CommandType.C_PUSH;
        this.segment = segment;
        this.element = element;
        this.fileName = fileName;
    }

    internal PushCommand(string segment, string element) : this(segment, element, "")
    {
    }


    public override string GetAsmCode()
    {
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
            pushToSP,
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
            pushToSP,
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
            pushToSP,
            incrSP

        };
        return JoinString(words);
    }

    string GetConstant()
    {
        string[] words = {
            $"// push constant {element}",
            $"@{element}",
            "D=A",
            pushToSP,
            incrSP
        };

        return JoinString(words);
    }
}