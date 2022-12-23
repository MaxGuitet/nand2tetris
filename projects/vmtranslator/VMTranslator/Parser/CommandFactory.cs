public class CommandFactory
{
    static Command GetCommand(string commandString)
    {
        string[] commandParts = commandString.Split(" ");

        if (commandParts.Length == 1)
        {
            return new ArithmeticCommand(commandParts);
        }
    }
}