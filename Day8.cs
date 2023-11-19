using System.Xml.Serialization;

namespace Advent_of_Code_2022;

public class Day8 : Day
{
    public Day8() : base(8)
    {
        var exampleData = Properties.Resources.ExampleDataDay8.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay8.Replace("\r\n", "\n").Split("\n");

        Compute(exampleData, out ExamplePart1, out ExamplePart2);
        Compute(data, out Part1, out Part2);
    }

    private void Compute(string[] data, out string part1, out string part2)
    {
        var treeMap = new TreeMap(data);

        treeMap.Print();

        part1 = treeMap.CountVisible().ToString();
        part2 = treeMap.Compute2().ToString();
    }


    public class TreeMap
    {
        public int[,] map;
        public bool[,] isVisible;
        public int Height;
        public int Width;

        private int[] _maxFromTop;
        private int[] _maxFromBottom;
        private int[] _maxFromLeft;
        private int[] _maxFromRight;

        public TreeMap(string[] data)
        {
            InitMap(data);
        }

        public void Print()
        {
            Console.WriteLine("Map:");

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write('\n');
            }

            Console.WriteLine("IsVisible:");

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Console.Write(isVisible[x, y] ? "x" : ".");
                }
                Console.Write('\n');
            }
        }

        public int CountVisible()
        {
            var visibleCount = 0;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (isVisible[x, y]) visibleCount++;
                }
            }
            return visibleCount;
        }

        public int Compute2()
        {
            var score = new int[Width, Height];

            for(var x = 0; x < Height; x++)
            {
                for(var y = 0; y < Width; y++)
                {
                    score[x, y] = WalkInAllDirections(x, y);
                }
            }

            var max = -1;

            for (var y = 0; y < Width; y++)
            {
                for (var x = 0; x < Height; x++)
                {
                    if(score[x, y] > max)
                    {
                        max = score[x, y];
                    }
                }
            }

            return max;
        }

        private int WalkInAllDirections(int x, int y)
        {
            var upResult = 0;
            var downResult = 0;
            var leftResult = 0;
            var rightResult = 0;

            // UP
            var currentX = x;
            var currentY = y;

            while(--currentY >= 0)
            {
                upResult++;

                if (map[x, y] <= map[currentX, currentY])
                {
                    break;
                }
            }


            // DOWN
            currentX = x;
            currentY = y;
            while (++currentY < Height)
            {
                downResult++;
                if (map[x, y] <= map[currentX, currentY])
                {
                    break;
                }
            }

            // LEFT
            currentX = x;
            currentY = y;
            while (--currentX >= 0)
            {
                leftResult++;
                if (map[x, y] <= map[currentX, currentY])
                {
                    break;
                }
            }

            // RIGHT
            currentX = x;
            currentY = y;
            while (++currentX < Width)
            {
                rightResult++;
                if (map[x, y] <= map[currentX, currentY])
                {
                    break;
                }
            }

            return upResult * downResult * leftResult * rightResult;
        }

        private void InitMap(string[] data)
        {
            Height = data.Length;
            Width = data[0].Length;

            map = new int[Width, Height];
            isVisible = new bool[Width, Height];

            _maxFromBottom = new int[Width];
            _maxFromTop = new int[Width];
            _maxFromLeft = new int[Height];
            _maxFromRight = new int[Width];

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    map[x, y] = data[y][x] - '0';

                    InitVisibilityAndMaxCollections(x, y);
                }
            }
            
            // left to right
            for (var y = 1; y < Height - 1; y++)
            {
                for (var x = 1; x < Width - 1; x++)
                {
                    if (map[x, y] > _maxFromLeft[y])
                    {
                        isVisible[x, y] = true;
                        _maxFromLeft[y] = map[x, y];
                    }
                }
            }

            // right to left
            for (var y = 1; y < Height - 1; y++)
            {
                for (var x = Width - 1; x > 0; x--)
                {
                    if (map[x, y] > _maxFromRight[y])
                    {
                        isVisible[x, y] = true;
                        _maxFromRight[y] = map[x, y];
                    }

                    if (_maxFromRight[y] >= _maxFromLeft[y])
                    {
                        break;
                    }
                }
            }

            // top to bottom
            for (var x = 1; x < Width - 1; x++)
            {
                for (var y = 1; y < Height - 1; y++)
                {
                    if (map[x, y] > _maxFromTop[x])
                    {
                        _maxFromTop[x] = map[x, y];
                        isVisible[x, y] = true;
                    }
                }
            }

            // bottom to top
            for (var x = 1; x < Width - 1; x++)
            {
                for (var y = Height - 1; y > 0; y--)
                {
                    if (map[x, y] > _maxFromBottom[x])
                    {
                        _maxFromBottom[x] = map[x, y];
                        isVisible[x, y] = true;
                    }

                    if (map[x, y] >= _maxFromTop[x])
                    {
                        break;
                    }
                }
            }
        }

        private void InitVisibilityAndMaxCollections(int x, int y)
        {
            if (x == 0)
            {
                isVisible[x, y] = true;
                _maxFromLeft[y] = map[x, y];
            }
            else if (x == Width - 1)
            {
                isVisible[x, y] = true;
                _maxFromRight[y] = map[x, y];
            }
            else if (y == 0)
            {
                isVisible[x, y] = true;
                _maxFromTop[x] = map[x, y];
            }
            else if (y == Height - 1)
            {
                isVisible[x, y] = true;
                _maxFromBottom[x] = map[x, y];
            }
        }
    }
}