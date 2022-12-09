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

    public bool IsAdjacent(in Position position)
        => Vector2.Distance(new Vector2(X, Y), new Vector2(position.X, position.Y)) < 2.0f;


}
file enum Direction
{
    Up = 'U',
    Down = 'D',
    Right = 'R',
    Left = 'L'

}
internal struct Problem9Part1 : IProblem
{
    public static int Id => 9;
    public static int Part => 1;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        Position tail = default;
        Position head = default;
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
                        if (!head.IsAdjacent(tail))
                        {
                            tail.X = head.X;
                            tail.Y = head.Y - 1;
                        }
                        break;
                    case Direction.Down:
                        head.Y--;
                        if (!head.IsAdjacent(tail))
                        {
                            tail.X = head.X;
                            tail.Y = head.Y + 1;
                        }
                        break;
                    case Direction.Right:
                        head.X++;
                        if (!head.IsAdjacent(tail))
                        {
                            tail.X = head.X - 1;
                            tail.Y = head.Y;
                        }
                        break;
                    case Direction.Left:
                        head.X--;
                        if (!head.IsAdjacent(tail))
                        {
                            tail.X = head.X + 1;
                            tail.Y = head.Y;
                        }
                        break;
                }

                visitedPositions.Add(tail);
            }

        }
        return visitedPositions.Count;
    }
}