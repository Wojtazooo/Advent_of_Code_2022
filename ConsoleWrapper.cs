namespace Advent_of_Code_2022;

public static class ConsoleWrapper
{
    public static void WriteLineColored(object objectToWriteLine, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor ?? Console.BackgroundColor;
        Console.WriteLine(objectToWriteLine);
        Console.ResetColor();
    }
}