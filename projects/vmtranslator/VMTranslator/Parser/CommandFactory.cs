public class CommandFactory
{
    internal static Command GetCommand(string commandString)
    {
        string[] commandParts = commandString.Split(" ");

        if (commandParts.Length == 1)
        {
            return new ArithmeticCommand(commandParts);
        }

        if (commandParts[0] == "push")
        {
            return new PushCommand(commandParts);
        }

        throw new InvalidCommandException($"Unknown command \"{commandString}\'");
    }
}