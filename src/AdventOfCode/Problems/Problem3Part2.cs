using AdventOfCode.Tokens;

namespace AdventOfCode.Problems;

internal struct Problem3Part2 : IProblem
{
    public static int Id => 3;
    public static int Part => 1;
    public static int Solve(ReadOnlySpan<byte> input)
    {
        var cursor = new Cursor(input);
        var totalScore = 0;
        do
        {
            var firstBackpack = cursor.ReadNextToken(true).Data;
            var secondBackpack = cursor.ReadNextToken(true).Data;
            var thirdBackpack = cursor.ReadNextToken(true).Data;
            foreach (var first in firstBackpack)
            {
                foreach (var second in secondBackpack)
                {
                    foreach (var third in thirdBackpack)
                    {
                        if (first == second && first == third)
                        {
                            totalScore += char.IsLower((char)first) ? first - 'a' + 1 : first - 'A' + 27;
                            goto End;
                        }
                    }
                }
            }
            End:;

        } while (cursor.HasMore);

        return totalScore;
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