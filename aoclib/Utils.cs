namespace aoclib;

public class Utils
{
    public static string GetTrimmedInput(string file)
    {
        return File.ReadAllText(file).Trim();
    }
    
}