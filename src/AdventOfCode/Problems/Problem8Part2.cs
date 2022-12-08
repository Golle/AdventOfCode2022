using System.Net;

namespace AdventOfCode.Problems;

internal struct Problem8Part2 : IProblem
{
    public static int Id => 8;
    public static int Part => 2;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        var width = input[0].Length;
        var height = input[1].Length;

        var grid = new int[width, height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                grid[y, x] = input[y][x] - '0';
            }
        }

        var max = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                max = Math.Max(GetVisibleTrees(x, y, grid), max);
            }
        }

        var val = GetVisibleTrees(2, 1, grid);
        var val1 = GetVisibleTrees(2, 3, grid);
        return max;


        static int GetVisibleTrees(int x, int y, int[,] grid)
        {
            if (x == 0 || y == 0 || x == grid.GetLength(1) - 1 || y == grid.GetLength(0) - 1)
            {
                return 0;
            }

            var value = grid[y, x];
            int left = 0, right = 0, up = 0, down = 0;

            for (var i = x - 1; i >= 0; --i)
            {
                if (grid[y, i] < value)
                {
                    left++;
                }
                else
                {
                    left++;
                    break;
                }
            }

            for (var i = x + 1; i < grid.GetLength(1); ++i)
            {
                if (grid[y, i] < value)
                {
                    right++;
                }
                else
                {
                    right++;
                    break;
                }
            }

            for (var i = y - 1; i >= 0; --i)
            {
                if (grid[i, x] < value)
                {
                    up++;
                }
                else
                {
                    up++;
                    break;
                }
            }

            for (var i = y + 1; i < grid.GetLength(0); ++i)
            {
                if (grid[i, x] < value)
                {
                    down++;
                }
                else
                {
                    down++;
                    break;
                }
            }

            return up * down * left * right;
        }


    }
}