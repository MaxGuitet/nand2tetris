public class CommandFactory
{
    internal static ICommand GetCommand(string[] commandParts, string fileName)
    {
        string instruction = commandParts[0];

        if (commandParts.Length == 1)
        {
            return new ArithmeticCommand(commandParts);
        }

        switch (instruction)
        {
            case "push":
                return new PushCommand(commandParts, fileName);

            case "pop":
                return new PopCommand(commandParts, fileName);

            case "label":
                return new LabelCommand(commandParts);

            case "goto":
                return new GotoCommand(commandParts);

            case "if-goto":
                return new IfgotoCommand(commandParts);
        }


        throw new InvalidCommandException($"Unknown command \"{string.Join(" ", commandParts)}\'");
    }
}