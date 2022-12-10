using System.Collections;
using System.Runtime.InteropServices;

namespace Advent_of_Code_2022
{
    public class Day1 : Day
    {
        public Day1() : base(1)
        {
            var exampleData = Properties.Resources.ExampleDataDay1.Replace("\r\n", "\n").Split("\n");
            var exampleDataTop3 = GetTopNCalories(exampleData, 3);
            ExamplePart1 = exampleDataTop3.First().ToString();
            ExamplePart2 = exampleDataTop3.Sum().ToString();

            var data = Properties.Resources.DataDay1.Replace("\r\n", "\n").Split("\n");
            var dataTop3 = GetTopNCalories(data, 3);
            Part1 = dataTop3.First().ToString();
            Part2 = dataTop3.Sum().ToString();
        }

        private List<int> GetTopNCalories(string[] data, int n)
        {
            var orderedTopNLinkedList = new LinkedList<int>();

            var currentElfCalories = 0;
            foreach (var line in data)
            {
                if (line == string.Empty)
                {
                    AddLineToLinkedList(n, orderedTopNLinkedList, currentElfCalories);
                    currentElfCalories = 0;
                    continue;
                }

                currentElfCalories += int.Parse(line);
            }

            AddLineToLinkedList(n, orderedTopNLinkedList, currentElfCalories);

            return orderedTopNLinkedList.ToList();
        }

        private static void AddLineToLinkedList(int n, LinkedList<int> topNLinkedList, int currentElfCalories)
        {
            var firstNodeLessThanCurrentValue = topNLinkedList.First;
            while (firstNodeLessThanCurrentValue != null && firstNodeLessThanCurrentValue.Value > currentElfCalories)
            {
                firstNodeLessThanCurrentValue = firstNodeLessThanCurrentValue.Next;
            }

            if (firstNodeLessThanCurrentValue == null)
            {
                topNLinkedList.AddLast(currentElfCalories);
            }
            else
            {
                topNLinkedList.AddBefore(firstNodeLessThanCurrentValue, currentElfCalories);
            }

            if (topNLinkedList.Count == n + 1)
            {
                topNLinkedList.RemoveLast();
            }
        }

        
    }
}
