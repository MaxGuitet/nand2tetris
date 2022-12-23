using System.Text.RegularExpressions;

public class Parser
{
    private StreamReader fileStream;
    private string filePath;
    private string? nextLine;

    public Parser(string inputFilePath)
    {
        filePath = inputFilePath;
        fileStream = new StreamReader(inputFilePath);
        InitParser();
    }

    private void InitParser()
    {
        ReadNextCommand();
    }

    private string CleanUpLine(string line)
    {
        // Clear spaces at the begining and end of line.
        string cleanLine = Regex.Replace(line, "^\\s+", "");
        cleanLine = Regex.Replace(cleanLine, "\\s+$", "");

        // clear comments
        cleanLine = Regex.Replace(cleanLine, "\\/\\/.*$", "");

        return cleanLine;
    }

    private bool CheckIsEmptyLine(string line)
    {
        return Regex.IsMatch(line, "^$");
    }

    private void ReadNextCommand()
    {
        bool isEmptyLine = false;
        string cleanLine = "";
        do
        {
            string? line = fileStream.ReadLine();

            if (line == null)
            {
                nextLine = null;
                return;
            }

            cleanLine = CleanUpLine(line);

            // Keep reading while it's an empty line (comment lines have been cleaned up by CleanUpLine)
            isEmptyLine = CheckIsEmptyLine(cleanLine);
        } while (isEmptyLine);

        nextLine = cleanLine;
    }

    public bool hasMoreCommands()
    {
        return nextLine != null;
    }

    public void advance()
    {
        currentCommand = CommandFactory.getCommand(nextLine);
    }
}