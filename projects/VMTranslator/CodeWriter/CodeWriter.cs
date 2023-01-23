public class CodeWriter
{
    private StreamWriter fileStream;

    public CodeWriter(string outputFilePath)
    {
        fileStream = new StreamWriter(outputFilePath);
    }

    public void AddBootstrapCode()
    {
        fileStream.WriteLine("@256");
        fileStream.WriteLine("D=A");
        fileStream.WriteLine("@SP");
        fileStream.WriteLine("M=D");
        // Initialize LCL = SP. We have the current SP value in D, simply assign it.
        fileStream.WriteLine("@LCL");
        fileStream.WriteLine("M=D");

        CallCommand callSysInit = new CallCommand("Sys.init", 0, "Sys");

        fileStream.WriteLine(callSysInit.GetAsmCode());
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