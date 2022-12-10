using System.ComponentModel;

namespace Advent_of_Code_2022;

public class Day3 : Day
{
    private const int SecondPartGroupSize = 3;

    public Day3() : base(3)
    {
        var exampleData = Properties.Resources.ExampleDataDay3.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay3.Replace("\r\n", "\n").Split("\n");

        var exampleRuckSacks = new List<Rucksack>(exampleData.Select(x => new Rucksack(x)));
        var ruckSacks = new List<Rucksack>(data.Select(x => new Rucksack(x)));

        ExamplePart1 = ComputeSumForPart1(exampleRuckSacks);
        Part1 = ComputeSumForPart1(ruckSacks);

        ExamplePart2 = ComputeSumForPart2(exampleRuckSacks);
        Part2 = ComputeSumForPart2(ruckSacks);
    }

    private static int ComputeSumForPart1(IReadOnlyList<Rucksack> rucksacks) =>
        rucksacks.Sum(x => GetPointsForChar(x.GetItemThatAppearsInBothCompartments()));

    private static int ComputeSumForPart2(IReadOnlyList<Rucksack> rucksacks)
    {
        var sum = 0;
        for (int i = 0; i < rucksacks.Count; i += SecondPartGroupSize)
        {
            var groupBadge = new List<char>(rucksacks[i].Items);

            for (int j = 0; j < SecondPartGroupSize; j++)
            {
                groupBadge = groupBadge.Intersect(rucksacks[j + i].Items).ToList();
            }

            sum += GetPointsForChar(groupBadge.Single());
        }

        return sum;
    }

    private static int GetPointsForChar(char character)
    {
        if (char.IsLower(character))
        {
            return (int)(character - 'a' + 1);
        }
        else
        {
            return (int)(character - 'A' + 27);
        }
    }

    public class Rucksack
    {
        public HashSet<char> Items;
        public HashSet<char> LeftCompartment;
        public HashSet<char> RightCompartment;

        public Rucksack(string rucksackItems)
        {
            Items = new HashSet<char>(rucksackItems);
            LeftCompartment = new HashSet<char>(rucksackItems.Take(rucksackItems.Length / 2));
            RightCompartment = new HashSet<char>(rucksackItems.TakeLast(rucksackItems.Length / 2));
        }

        public char GetItemThatAppearsInBothCompartments() => LeftCompartment.Intersect(RightCompartment).Single();
    }
}