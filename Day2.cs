using System.Diagnostics;

namespace Advent_of_Code_2022;

public class Day2 : Day
{
    public enum Move
    {
        Rock,
        Scissors,
        Paper
    }

    private static readonly Dictionary<Move, int> PlayedMovePoints = new Dictionary<Move, int>
    {
        {Move.Rock, 1},
        {Move.Paper, 2},
        {Move.Scissors, 3},
    };

    private const int WinPoints = 6;
    private const int DrawPoints = 3;
    private const int LosePoints = 0;

    public Day2() : base(2)
    {
        var exampleData = Properties.Resources.ExampleDataDay2.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay2.Replace("\r\n", "\n").Split("\n");

        ExamplePart1 = FirstPart.SummarizeMoves(exampleData);
        Part1 = FirstPart.SummarizeMoves(data);

        ExamplePart2 = SecondPart.SummarizeMoves(exampleData);
        Part2 = SecondPart.SummarizeMoves(data);
    }

    private static int GetPlayerPoints(Move opponentMove, Move playerMove)
    {
        if (opponentMove == playerMove) return DrawPoints;

        switch ((opponentMove, playerMove))
        {
            case (Move.Rock, Move.Paper): return WinPoints;
            case (Move.Rock, Move.Scissors): return LosePoints;
            case (Move.Paper, Move.Scissors): return WinPoints;
            case (Move.Paper, Move.Rock): return LosePoints;
            case (Move.Scissors, Move.Rock): return WinPoints;
            case (Move.Scissors, Move.Paper): return LosePoints;
            default: throw new ArgumentOutOfRangeException();
        }
    }


    public static class FirstPart
    {
        private static readonly Dictionary<char, Move> MoveTranslations = new Dictionary<char, Move>
        {
            {'A', Move.Rock},
            {'B', Move.Paper},
            {'C', Move.Scissors},
            {'X', Move.Rock},
            {'Y', Move.Paper},
            {'Z', Move.Scissors},
        };

        public static int SummarizeMoves(string[] movesData)
        {
            var sum = 0;
            foreach (var move in movesData)
            {
                var opponentMove = MoveTranslations[move[0]];
                var playerMove = MoveTranslations[move[2]];

                sum += GetPlayerPoints(opponentMove, playerMove) + PlayedMovePoints[playerMove];
            }

            return sum;
        }
    }

    public static class SecondPart
    {
        private static readonly Dictionary<char, Move> MoveTranslations = new Dictionary<char, Move>
        {
            {'A', Move.Rock},
            {'B', Move.Paper},
            {'C', Move.Scissors},
        };

        public static int SummarizeMoves(string[] movesData)
        {
            var sum = 0;
            foreach (var move in movesData)
            {
                var opponentMove = MoveTranslations[move[0]];
                var playerMove = FindPlayerMove(opponentMove, move[2]);

                sum += GetPlayerPoints(opponentMove, playerMove) + PlayedMovePoints[playerMove];
            }

            return sum;
        }

        private static Move FindPlayerMove(Move opponentMove, char command)
        {
            switch (command)
            {
                case 'X':
                    return GetLoseMove(opponentMove);
                case 'Y':
                    return opponentMove;
                case 'Z':
                    return GetWinMove(opponentMove);
                default:
                    throw new ArgumentOutOfRangeException(nameof(command));
            };
        }

        private static Move GetWinMove(Move move)
        {
            switch (move)
            {
                case Move.Paper: return Move.Scissors;
                case Move.Rock: return Move.Paper;
                case Move.Scissors: return Move.Rock;
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }

        private static Move GetLoseMove(Move move)
        {
            switch (move)
            {
                case Move.Paper: return Move.Rock;
                case Move.Rock: return Move.Scissors;
                case Move.Scissors: return Move.Paper;
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}