class Compiler
{
    private static string inputPath = "";

    public static void Main(string[] args)
    {
        inputPath = Path.GetFullPath("/home/max/Projects/nand2tetris/projects/09/Average/Main.jack");

        if (args.Length == 1)
        {
            inputPath = Path.GetFullPath(args[0]);
        }

        string[] files = IoUtils.ExtractFiles(inputPath, ".jack");
        string outputFileName = IoUtils.GetOutputFilePath(inputPath, ".xml");

        CompileFiles(files);
    }

    private static void CompileFiles(string[] files)
    {
        foreach (string file in files)
        {
            CompileFile(file);
        }
    }

    private static void CompileFile(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        JackTokenizer tokenizer = new JackTokenizer(filePath);

        while (tokenizer.HasMoreTokens())
        {
            IToken token = tokenizer.Advance();
        }
    }
}