namespace Advent_of_Code_2022;

public class Day6 : Day
{
    public const int FirstPartMarkerLength = 4;
    public const int SecondPartMarkerLength = 14;

    public Day6() : base(6)
    {
        var exampleData = Properties.Resources.ExampleDataDay6.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay6.Replace("\r\n", "\n").Split("\n");

        ExamplePart1 = (FindFirstMarker(exampleData[0], FirstPartMarkerLength) + 1).ToString();
        Part1 = (FindFirstMarker(data[0], FirstPartMarkerLength)).ToString();

        ExamplePart2 = (FindFirstMarker(exampleData[0], SecondPartMarkerLength) + 1).ToString();
        Part2 = (FindFirstMarker(data[0], SecondPartMarkerLength)).ToString();
    }

    public int FindFirstMarker(string input, int length)
    {
        var currentWindow = new LinkedList<char>();

        for (int i = 0; i < length; i++)
        {
            currentWindow.AddLast(input[i]);
        }

        for (var i = length; i < input.Length; i++)
        {
            if (new HashSet<char>(currentWindow).Count == length)
            {
                return i;
            }

            currentWindow.AddLast(input[i]);
            currentWindow.RemoveFirst();
        }

        return -1;
    }


}