using AdventOfCode.Tokens;
using System.Diagnostics;

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
        var cursor = new Cursor(input);
        Span<Pair> pairs = stackalloc Pair[2];
        var count = 0;
        do
        {
            foreach (ref var pair in pairs)
            {
                pair.Start = cursor.ReadNextToken().AsInteger();
                var token = cursor.ReadNextToken();
                Debug.Assert(token.Type is TokenType.Dash);
                pair.End = cursor.ReadNextToken().AsInteger();
                var endToken = cursor.ReadNextToken();
                Debug.Assert(endToken.Type is TokenType.NewLine or TokenType.Comma or TokenType.Invalid);
            }
            if (pairs[0].Overlaps(pairs[1]))
            {
                count++;
            }

        } while (cursor.HasMore);

        return count;
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