public class CodeWriter
{
    private StreamWriter fileStream;

    public CodeWriter(string outputFilePath)
    {
        fileStream = new StreamWriter(outputFilePath);
    }

    public void WriteCommand(ICommand command)
    {
        string asmCode = command.GetAsmCode();
        fileStream.WriteLine(asmCode);
    }

    public void End()
    {
        fileStream.Close();
    }
}