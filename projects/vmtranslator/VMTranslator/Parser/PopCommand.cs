using System.ComponentModel;
using System.Globalization;

internal class PopCommand : ICommand
{
    static string TEMP = "5";
    string incrSP = "@SP\nM=M+1";
    string decrSP = "@SP\nM=M-1";
    string fileName;
    string[] parts;
    internal PopCommand(string[] commandParts, string fileName)
    {
        type = CommandType.C_POP;
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

                return GetGeneric(segment, TEMP, element);

            default:
                throw new Exception($"Unknown segment for pop instruction \"{segment}\".");
        }
    }

    private string GetGeneric(string segmentName, string pointerName, string element)
    {
        string[] words = {
            $"// pop {segmentName} {element}",
            decrSP,
            // read last value on stack and store in D
            "@SP",
            "A=M",
            "D=M",
            $"@{pointerName}",
            "D=M",
            $"@{element}",
            "D=D+A",
            // Go to address POINTER + i
            "A=D",
            // store value to pop
            "D=M",

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

    private string JoinString(params string[] words)
    {
        return string.Join("\n", words);
    }
}