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


    public bool Overlaps(in Pair pair)
    {

        if (End < pair.Start)
        {
            return false;
        }

        if (Start > pair.End)
        {
            return false;
        }

        return true;
    }
}
internal struct Problem4Part2 : IProblem
{
    public static int Id => 4;
    public static int Part => 2;
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

            if (pair1.Overlaps(pair2))
            {
                count ++;
            }
        }

        return count;
    }
}