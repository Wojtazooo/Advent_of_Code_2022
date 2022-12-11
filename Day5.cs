using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2022;

public class Day5 : Day
{
    public Day5() : base(5)
    {
        var exampleData = Properties.Resources.ExampleDataDay5.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay5.Replace("\r\n", "\n").Split("\n");

        var exampleCargoCrane = new CargoCrane(exampleData);
        var cargoCrane = new CargoCrane(data);

        ExamplePart1 = new string(exampleCargoCrane.SimulateMoves(false).Select(x => x.Peek()).ToArray());
        Part1 = new string(cargoCrane.SimulateMoves(false).Select(x => x.Peek()).ToArray());

        ExamplePart2 = new string(exampleCargoCrane.SimulateMoves(true).Select(x => x.Peek()).ToArray());
        Part2 = new string(cargoCrane.SimulateMoves(true).Select(x => x.Peek()).ToArray());
    }

    public class CargoCrane
    {
        private Stack<char>[] _stacks;

        private List<Move> _moves;

        public CargoCrane(string[] data)
        {
            FillStacks(data);
            FillMoves(data);
        }

        public record Move(int times, int from, int to);

        public List<Stack<char>> SimulateMoves(bool allAtOnce)
        {
            var result = new List<Stack<char>>();
            for (var i = 0; i < _stacks.Length; i++)
            {
                result.Add(new Stack<char>(_stacks[i].Reverse()));
            }

            if (allAtOnce)
            {
                _moves.ForEach(x => ExecuteMoveAllAtOnce(result, x));
            }
            else
            {
                _moves.ForEach(x => ExecuteMoveOneByOne(result, x));
            }

            return result;;
        }

        private static void ExecuteMoveOneByOne(IReadOnlyList<Stack<char>> stacks, Move move)
        {
            for (var t = 0; t < move.times; t++)
            {
                stacks[move.to - 1].Push(stacks[move.from - 1].Pop());
            }
        }

        private static void ExecuteMoveAllAtOnce(IReadOnlyList<Stack<char>> stacks, Move move)
        {
            var temp = new Stack<char>();
            for (var t = 0; t < move.times; t++)
            {
                temp.Push(stacks[move.from - 1].Pop());
            }

            for (var t = 0; t < move.times; t++)
            {
                stacks[move.to - 1].Push(temp.Pop());
            }
        }

        private void FillStacks(string[] data)
        {
            FindSize(data, out var size, out var yIndex);
            InitStacks(size);

            for (var y = yIndex - 1; y >= 0; y--)
            {
                for (var stack = 0; stack < size; stack += 1)
                {
                    var x = stack * 4 + 1;
                    if (!char.IsWhiteSpace(data[y][x]))
                    {
                        _stacks[stack].Push(data[y][x]);
                    }
                }
            }
        }

        private void FillMoves(string[] data)
        {
            _moves = new List<Move>();

            foreach (var t in data)
            {
                if (t.StartsWith("move"))
                {
                    var values = t
                        .Replace("move ", "")
                        .Replace("from ", "")
                        .Replace("to ", "")
                        .Split(" ");

                    _moves.Add(new Move(
                        int.Parse(values[0].Trim()),
                        int.Parse(values[1].Trim()),
                        int.Parse(values[2].Trim())));
                }
            }
        }

        private void FindSize(string[] data, out int size, out int sizeDescriptionIndex)
        {
            size = 0;
            for (sizeDescriptionIndex = 0; sizeDescriptionIndex < data.Length; sizeDescriptionIndex++)
            {
                var line = data[sizeDescriptionIndex];
                if (line.Length > 1 && char.IsDigit(line[1]))
                {
                    size = int.Parse(line.Trim().Split(" ").Last());
                    break;
                }
            }
        }

        private void InitStacks(int size)
        {
            _stacks = new Stack<char>[size];
            for (var i = 0; i < size; i++)
            {
                _stacks[i] = new Stack<char>();
            }
        }
    }
}