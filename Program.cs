﻿namespace Advent_of_Code_2022
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var days = new List<Day>
            {
                new Day1(),
                new Day2(),
                new Day3(),
                new Day4(),
                new Day5(),
                new Day6(),
                new Day7(),
                new Day8(),
            };
            foreach (var day in days)
            {
                Console.WriteLine("=====================================");
                day.PrintOutput();
            }
        }
    }
}