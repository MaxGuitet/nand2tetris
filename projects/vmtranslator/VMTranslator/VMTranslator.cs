public class VMTranslator
{
    private static CodeWriter? writer;
    private static string currentFileName = "";
    private static string inputPath = "";

    public static void Main(string[] args)
    {
        inputPath = Path.GetFullPath("/home/max/Projects/nand2tetris/projects/08/FunctionCalls/NestedCall");

        if (args.Length == 1)
        {
            inputPath = Path.GetFullPath(args[0]);
        }

        string[] files = IoUtils.ExtractFiles(inputPath, ".vm");
        string outputFileName = IoUtils.GetOutputFilePath(inputPath, ".asm");

        writer = new CodeWriter(outputFileName);

        // writer.AddBootstrapCode();

        ParseFiles(files);

        writer.End();
    }

    private static void ParseFiles(string[] files)
    {
        foreach (string file in files)
        {
            ParseFile(file);
        }
    }

    private static void ParseFile(string filePath)
    {
        currentFileName = Path.GetFileNameWithoutExtension(filePath);
        Parser parser = new Parser(filePath, currentFileName);

        if (writer == null)
        {
            throw new Exception("Missing writer.");
        }

        while (parser.HasMoreCommands())
        {
            string[] commandParts = parser.Advance();
            ICommand currentCommand = CommandFactory.GetCommand(commandParts, currentFileName);
            writer.WriteCommand(currentCommand);
        }
    }
}