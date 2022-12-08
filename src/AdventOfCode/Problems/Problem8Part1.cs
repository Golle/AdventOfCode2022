namespace AdventOfCode.Problems;

internal struct Problem8Part1 : IProblem
{
    public static int Id => 8;
    public static int Part => 1;
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

        var count = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (IsVisible(x, y, grid))
                {
                    count++;
                    //Console.Write("X");
                }
                else 
                {
                    //Console.Write("O");
                }
                
            }
            //Console.WriteLine();
        }

        return count;


        static bool IsVisible(int x, int y, int[,] grid)
        {
            if (x == 0 || y == 0 || x == grid.GetLength(1) - 1 || y == grid.GetLength(0) - 1)
            {
                return true;
            }

            var value = grid[y, x];
            bool left, right, up, down;
            left = right = up = down = true;

            for (var i = x-1; i >= 0; --i)
            {
                if (grid[y, i] >= value)
                {
                    left = false;
                }
            }

            for (var i = x+1; i < grid.GetLength(1); ++i)
            {
                if (grid[y, i] >= value)
                {
                    right = false;
                }
            }

            for (var i = y-1; i >= 0; --i)
            {
                if (grid[i, x] >= value)
                {
                    up = false;
                }
            }

            for (var i = y+1; i < grid.GetLength(0); ++i)
            {
                if (grid[i, x] >= value)
                {
                    down = false;
                }
            }

            return left || right || up || down;
        }


    }
}