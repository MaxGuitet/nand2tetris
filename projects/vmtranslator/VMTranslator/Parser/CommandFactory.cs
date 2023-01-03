public class CommandFactory
{
    internal static ICommand GetCommand(string commandString, string fileName)
    {
        string[] commandParts = commandString.Split(" ");

        if (commandParts.Length == 1)
        {
            return new ArithmeticCommand(commandParts);
        }

        if (commandParts[0] == "push")
        {
            return new PushCommand(commandParts, fileName);
        }

        throw new InvalidCommandException($"Unknown command \"{commandString}\'");
    }
}