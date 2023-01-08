internal class PopCommand : ICommand
{
    private static string TEMP = "5";
    private string decrSP = "@SP\nM=M-1";
    private string fileName;
    private string segment;
    private string element;

    internal PopCommand(string segment, string element, string fileName)
    {
        type = CommandType.C_POP;
        this.segment = segment;
        this.element = element;
        this.fileName = fileName;
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
                throw new Exception($"Unknown segment for pop instruction \"{segment}\".");
        }
    }

    private string GetGeneric(string segmentName, string pointerName, string element)
    {
        string[] words = {
            $"// pop {segmentName} {element}",
            $"@{pointerName}",
            "D=M",
            $"@{element}",
            "D=D+A",
            // Store value in R13 for future use
            // R13 does not belong to any segment and is free to use
            "@R13",
            "M=D",
            decrSP,
            // read last value on stack and store in D
            "@SP",
            "A=M",
            "D=M",
            "@R13",
            // Go to address POINTER + i
            "A=M",
            // store value to pop
            "M=D",

        };
        return JoinString(words);
    }

    // GetTemp is almost the same than GetGeneric, but the second instruction
    // is D=A instead of D=M. 
    // Indeed, we know that the TEMP segment starts at 5 but we don't have symbol for it
    private string GetTemp(string element)
    {
        string[] words = {
            $"// pop temp {element}",
            $"@{TEMP}",
            "D=A",
            $"@{element}",
            "D=D+A",
            // Store value in R13 for future use
            // R13 does not belong to any segment and is free to use
            "@R13",
            "M=D",
            decrSP,
            // read last value on stack and store in D
            "@SP",
            "A=M",
            "D=M",
            "@R13",
            // Go to address POINTER + i
            "A=M",
            // store value to pop
            "M=D",

        };
        return JoinString(words);
    }

    private string GetDirectMemoryValue(string segmentName, string pointerName, string element)
    {

        string[] words = {
            $"// pop {segmentName} {element}",
            decrSP,
            // store value to pop
            "@SP",
            "A=M",
            "D=M",
            // pop to segment
            $"@{pointerName}",
            "M=D"
        };
        return JoinString(words);
    }
}