internal static class IoUtils
{
    internal static string[] ExtractFiles(string inputPath, string extension = ".vm")
    {
        // check if the inputPath is a directory
        if (Directory.Exists(inputPath))
        {
            string[] files = Directory.GetFiles(inputPath, "*" + extension);
            if (files.Length == 0)
            {
                throw new IOException("No .vm files found in the given directory.");
            }

            return files;
        }

        // check if the inputPath is a file
        if (File.Exists(inputPath))
        {
            // check if the file is a .vm file
            if (!inputPath.EndsWith(".jack"))
            {
                throw new IOException("Expecting a .jack file as input.");
            }

            return new string[] { inputPath };
        }

        throw new IOException("Invalid input path.");
    }

    internal static string GetOutputFilePath(string inputPath, string extension = ".asm")
    {
        string directoryPath;
        string fileName;

        if (inputPath == "" || inputPath == null)
        {
            throw new IOException("Invalid input path.");
        }

        // If the inputPath is a directory, the output file will be named after the directory
        if (Directory.Exists(inputPath))
        {
            fileName = Path.GetFileName(inputPath);
            directoryPath = inputPath;
        }
        else
        {
            directoryPath = Path.GetDirectoryName(inputPath) ?? "";

            if (directoryPath == "")
            {
                throw new IOException("Invalid input path.");
            }

            fileName = Path.GetFileNameWithoutExtension(inputPath);
        }

        return Path.Join(directoryPath, fileName + extension);
    }
}