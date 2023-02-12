class Compiler
{
    private static string inputPath = "";
    private static StreamWriter writer;

    public static void Main(string[] args)
    {
        inputPath = Path.GetFullPath("/home/max/Projects/nand2tetris/projects/09/Average/Main.jack");

        if (args.Length == 1)
        {
            inputPath = Path.GetFullPath(args[0]);
        }

        string[] files = IoUtils.ExtractFiles(inputPath, ".jack");

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
        string outputFilePath = IoUtils.GetOutputFilePath(inputPath, ".xml");
        JackTokenizer tokenizer = new JackTokenizer(filePath);

        // TODO: This is a temporary solution will be replaced by the actual Code generator in next module
        StreamWriter writer = new StreamWriter(outputFilePath);


        while (tokenizer.HasMoreTokens())
        {
            IToken token = tokenizer.Advance();
            writer.WriteLine(token.GetXMLCode());
        }
        writer.Close();
    }
}