namespace AdventOfCode.Problems;

internal struct Problem3Part1 : IProblem
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