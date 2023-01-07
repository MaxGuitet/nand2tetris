using System;

public class VMTranslator
{
    private static Parser? parser;
    private static CodeWriter? writer;
    private static string currentFileName = "";
    public static void Main(string[] args)
    {
        // string inputFilePath = "/home/max/Projects/nand2tetris/projects/07/StackArithmetic/StackTest/StackTest.vm";
        string inputFilePath = "/home/max/Projects/nand2tetris/projects/07/MemoryAccess/PointerTest/PointerTest.vm";

        if (args.Length == 1)
        {
            inputFilePath = args[0];
        }

        if (!inputFilePath.EndsWith(".vm"))
        {
            throw new IOException("Expecting a .vm file as input.");
        }

        currentFileName = Path.GetFileNameWithoutExtension(inputFilePath);
        parser = new Parser(inputFilePath, currentFileName);

        string outputFilePath = inputFilePath.Replace(".vm", ".asm");
        writer = new CodeWriter(outputFilePath);

        ParseFile();
    }

    private static void ParseFile()
    {
        if (parser == null)
        {
            throw new Exception("No parser provided.");
        }

        if (writer == null)
        {
            throw new Exception("No writer provided.");
        }

        while (parser.HasMoreCommands())
        {
            string[] commandParts = parser.Advance();
            ICommand currentCommand = CommandFactory.GetCommand(commandParts, currentFileName);
            writer.WriteCommand(currentCommand);
        }

        writer.End();
    }
}