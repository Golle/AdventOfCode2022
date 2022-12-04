namespace AdventOfCode.Problems;


file struct Pair
{
    public int Start;
    public int End;

    public Pair(int start, int end)
    {
        Start = start;
        End = end;
    }


    public bool Contains(in Pair pair)
    {
        if (pair.Start < Start)
        {
            return false;
        }
        if (pair.End > End)
        {
            return false;
        }
        return true;
    }
}

internal struct Problem4Part1 : IProblem
{
    public static int Id => 4;
    public static int Part => 1;
    public static int Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        var count = 0;
        foreach (var ina in input)
        {
            var splits = ina.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var pair1Values = splits[0].Split("-").Select(int.Parse).ToArray();
            var pair2Values = splits[1].Split("-").Select(int.Parse).ToArray();
            var pair1 = new Pair(pair1Values[0], pair1Values[1]);
            var pair2 = new Pair(pair2Values[0], pair2Values[1]);

            if (pair1.Contains(pair2) || pair2.Contains(pair1))
            {
                count++;
            }
        }

        return count;
    }
}