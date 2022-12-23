class ArithmeticCommand : Command
{
    string operation;

    internal ArithmeticCommand(string[] commandParts)
    {
        type = CommandType.C_ARITHMETIC;
        operation = commandParts[0];
    }
    public override string getAsmCode()
    {
        // TODO
        switch (operation)
        {
            case "add":
                return "add";

            default:
                return "";
        }
    }
}