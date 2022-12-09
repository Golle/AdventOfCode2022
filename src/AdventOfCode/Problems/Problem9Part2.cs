using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Problems;

file struct Position
{
    public int X;
    public int Y;

    public readonly bool IsAdjacent(in Position position)
        => Vector2.Distance(new Vector2(X, Y), new Vector2(position.X, position.Y)) < 2.0f;


}
file enum Direction
{
    Up = 'U',
    Down = 'D',
    Right = 'R',
    Left = 'L'

}
internal struct Problem9Part2 : IProblem
{
    public static int Id => 9;
    public static int Part => 2;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        Span<Position> positions = new Position[10];
        ref var head = ref positions[0];

        HashSet<Position> visitedPositions = new();

        foreach (var ina in input)
        {
            var splits = ina.Split(" ");
            var direction = (Direction)splits[0][0];
            var count = int.Parse(splits[1]);
            for (var i = 0; i < count; ++i)
            {
                switch (direction)
                {
                    case Direction.Up:
                        head.Y++;
                        break;
                    case Direction.Down:
                        head.Y--;
                        break;
                    case Direction.Right:
                        head.X++;
                        break;
                    case Direction.Left:
                        head.X--;
                        break;
                }
                UpdatePositions(direction, positions);

                visitedPositions.Add(positions[8]);
                for (var y = -10; y < 20; ++y)
                {
                    for (var x = -10; x < 20; ++x)
                    {
                        if (positions.ToArray().Any(p => p.X == x && p.Y == y))
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write(".");

                        }
                    }
                    Console.WriteLine();
                }
                Thread.Sleep(60);
            }

        }

        

        return visitedPositions.Count;

        static void UpdatePositions(Direction direction, Span<Position> positions)
        {
            for (var i = 0; i < positions.Length - 1; ++i)
            {
                ref readonly var current = ref positions[i];
                ref var next = ref positions[i + 1];
                if (current.IsAdjacent(next))
                {
                    // all positions have been updated
                    return;
                }

                switch (direction)
                {
                    case Direction.Up:
                        next.Y++;
                        break;
                    case Direction.Down:
                        next.Y--;
                        break;
                    case Direction.Left:
                        next.X--;
                        break;
                    case Direction.Right:
                        next.X++;
                        break;
                }
            }
        }
    }



}