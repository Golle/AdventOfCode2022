using System.Diagnostics;

namespace AdventOfCode.Problems;

internal struct Problem3Part2 : IProblem
{
    public static int Id => 3;
    public static int Part => 1;
    public static int Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        var totalScore = 0;
        for (var i = 0; i < input.Length; i += 3)
        {
            var score = 0;
            foreach (var first in input[i])
            {
                foreach (var second in input[i + 1])
                {
                    foreach (var third in input[i + 2])
                    {
                        if (first == second && first == third)
                        {
                            score = char.IsLower(first) ? first - 'a' + 1 : first - 'A' + 27;
                            break;
                        }
                    }
                    if (score > 0)
                    {
                        break;
                    }
                }
                if (score > 0)
                {
                    break;
                }
            }
            totalScore += score;
        }
        return totalScore;
    }
}