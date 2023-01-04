using System;

public class VMTranslator
{
    private static Parser parser;
    private static CodeWriter writer;
    public static void Main(string[] args)
    {
        // string inputFilePath = "/home/max/Projects/nand2tetris/projects/07/StackArithmetic/StackTest/StackTest.vm";
        string inputFilePath = "/home/max/Projects/nand2tetris/projects/07/MemoryAccess/PointerTest/PointerTest.vm";

        if (!inputFilePath.EndsWith(".vm"))
        {
            throw new IOException("Expecting a .vm file as input.");
        }

        parser = new Parser(inputFilePath);

        string outputFilePath = inputFilePath.Replace(".vm", ".asm");
        writer = new CodeWriter(outputFilePath);

        ParseFile();
    }

    private static void ParseFile()
    {
        while (parser.HasMoreCommands())
        {
            ICommand currentCommand = parser.Advance();
            writer.WriteCommand(currentCommand);
        }

        writer.End();
    }
}