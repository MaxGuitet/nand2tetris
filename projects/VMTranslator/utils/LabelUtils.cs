static class LabelUtils
{
    public static string GetLabel(string label, string fileName = "", string functionName = "")
    {
        if (fileName == "")
        {
            return label;
        }

        if (functionName == "")
        {
            return $"{fileName}${label}";
        }

        // FunctionnName contains the fileName in it. e.g. Sys.main
        return $"{functionName}${label}";
    }
}