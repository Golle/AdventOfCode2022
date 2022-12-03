using AdventOfCode.Tokens;

namespace AdventOfCode.Problems;

internal struct Problem3Part1 : IProblem
{
    public static int Id => 3;
    public static int Part => 1;
    public static int Solve(ReadOnlySpan<byte> input)
    {
        var cursor = new Cursor(input);
        var totalScore = 0;
        do
        {
            var token = cursor.ReadNextToken(true);
            var half = token.Data.Length / 2;
            var firstCompartment = token.Data[..half];
            var secondCompartment = token.Data[half..];

            var score = 0;
            foreach (var first in firstCompartment)
            {
                foreach (var second in secondCompartment)
                {
                    if (first == second)
                    {
                        score += char.IsLower((char)first) ? first - 'a' + 1 : first - 'A' + 27;
                        break;
                    }
                }

                if (score > 0)
                {
                    break;
                }
            }
            totalScore += score;
        } while (cursor.HasMore);

        return totalScore;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        var totalScore = 0;
        foreach (var ina in input)
        {
            var half = ina.Length / 2;
            var firstCompartment = ina.Substring(0, half);
            var secondCompartment = ina.Substring(half);

            var score = 0;
            foreach (var item in firstCompartment)
            {
                foreach (var secondItem in secondCompartment)
                {
                    if (item == secondItem)
                    {
                        score = char.IsLower(item) ? item - 'a' + 1 : item - 'A' + 27;
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