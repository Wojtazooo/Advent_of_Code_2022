namespace Advent_of_Code_2022
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var days = new List<Day>
            {
                new Day1(),
            };
            foreach (var day in days)
            {
                Console.WriteLine("=====================================");
                day.PrintOutput();
            }
        }
    }
}