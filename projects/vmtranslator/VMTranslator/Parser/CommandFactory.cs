public class CommandFactory
{
    internal static ICommand GetCommand(string[] commandParts, string fileName)
    {
        string instruction = commandParts[0];

        ArithmeticOperation arithmeticOperation;
        if (Enum.TryParse<ArithmeticOperation>(instruction, out arithmeticOperation))
        {
            return new ArithmeticCommand(arithmeticOperation);
        }

        switch (instruction)
        {
            case "push":
                return new PushCommand(commandParts[1], commandParts[2], fileName);

            case "pop":
                return new PopCommand(commandParts[1], commandParts[2], fileName);

            case "label":
                return new LabelCommand($"{fileName}${commandParts[1]}");

            case "goto":
                return new GotoCommand($"{fileName}${commandParts[1]}");

            case "if-goto":
                return new IfgotoCommand(commandParts[1]);

            case "function":
                int nLocals = int.Parse(commandParts[2]);
                return new FunctionCommand(commandParts[1], nLocals);

            case "call":
                int nArgs = int.Parse(commandParts[2]);
                return new CallCommand(commandParts[1], nArgs, fileName);

            case "return":
                return new ReturnCommand();
        }


        throw new InvalidCommandException($"Unknown command \"{string.Join(" ", commandParts)}\'");
    }
}