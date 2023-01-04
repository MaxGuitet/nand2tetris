public class CommandFactory
{
    internal static ICommand GetCommand(string commandString, string fileName)
    {
        string[] commandParts = commandString.Split(" ");

        string instruction = commandParts[0];

        if (commandParts.Length == 1)
        {
            return new ArithmeticCommand(commandParts);
        }

        if (instruction == "push")
        {
            return new PushCommand(commandParts, fileName);
        }

        if (instruction == "pop")
        {
            return new PopCommand(commandParts, fileName);
        }

        throw new InvalidCommandException($"Unknown command \"{commandString}\'");
    }
}