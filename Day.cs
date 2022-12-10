namespace Advent_of_Code_2022
{
    public abstract class Day
    {
        public int DayNumber;

        public int ExamplePart1;

        public int ExamplePart2;

        public int Part1;

        public int Part2;

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
