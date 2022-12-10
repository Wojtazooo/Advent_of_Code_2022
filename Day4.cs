using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2022;

public class Day4 : Day
{
    public Day4() : base(4)
    {
        var exampleData = Properties.Resources.ExampleDataDay4.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay4.Replace("\r\n", "\n").Split("\n");

        var exampleAreaPairs = exampleData.Select(ParseAreaPair);
        var areaPairs = data.Select(ParseAreaPair);

        ExamplePart1 = exampleAreaPairs.Count(x => x.OneAreaFullyContainsSecond());
        Part1 = areaPairs.Count(x => x.OneAreaFullyContainsSecond());

        ExamplePart2 = exampleAreaPairs.Count(x => x.AreasOverlap());
        Part2 = areaPairs.Count(x => x.AreasOverlap());
    }

    private AreaPair ParseAreaPair(string line)
    {
        var pairs = line.Split(',');

        var firstPairSplit = pairs[0].Split('-');
        var secondPairSplit = pairs[1].Split('-');

        var area1 = new Area(int.Parse(firstPairSplit[0]), int.Parse(firstPairSplit[1]));
        var area2 = new Area(int.Parse(secondPairSplit[0]), int.Parse(secondPairSplit[1]));

        return new AreaPair(area1, area2);
    }

    public record Area(int From, int To)
    {
        public int Size => To - From;

        public bool IsInside(int value) => From <= value && value <= To;
    }

    public class AreaPair
    {
        public Area Area1;

        public Area Area2;


        public AreaPair(Area area1, Area area2)
        {
            Area1 = area1;
            Area2 = area2;
        }

        public bool OneAreaFullyContainsSecond()
        {
            var biggerArea = Area1.Size > Area2.Size ? Area1 : Area2;
            var secondArea = biggerArea == Area1 ? Area2 : Area1;

            return biggerArea.From <= secondArea.From && secondArea.To <= biggerArea.To;
        }

        public bool AreasOverlap()
        {
            var biggerArea = Area1.Size > Area2.Size ? Area1 : Area2;
            var secondArea = biggerArea == Area1 ? Area2 : Area1;

            return biggerArea.IsInside(secondArea.From) || biggerArea.IsInside(secondArea.To);
        }
    }
}