namespace Advent_of_Code_2022
{
    public abstract class Day
    {
        public int DayNumber;

        public string ExamplePart1;

        public string ExamplePart2;

        public string Part1;

        public string Part2;

        protected Day(int dayNumber)
        {
            DayNumber = dayNumber;
        }

        public void PrintOutput()
        {
            Console.WriteLine($"Results for day {DayNumber}");
            Console.WriteLine($"Example Part 1 - {ExamplePart1}");
            Console.WriteLine($"Part 1 - {Part1}");
            Console.WriteLine($"Example Part 2 - {ExamplePart2}");
            Console.WriteLine($"Part 2 - {Part2}");
        }
    }
}
