using System.Diagnostics;

namespace AdventOfCode.Problems;

internal struct Problem6Part2 : IProblem
{
    public static int Id => 6;
    public static int Part => 2;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        const int SequenceSize = 14;
        for (var outer = 0; outer < input.Length - SequenceSize; ++outer)
        {
            var found = true;
            ulong value = 0;
            for (var inner = outer; inner < outer + SequenceSize; ++inner)
            {
                var bitMask = 1ul << input[inner] - 'a';
                if ((value & bitMask) > 0)
                {
                    found = false;
                    break;
                }
                value |= bitMask;
            }
            if (found)
            {
                return outer + SequenceSize;
            }
        }
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        Debug.Assert(input.Length == 1);
        var dataStream = input[0];
        for (var i = 0; i < dataStream.Length-3; i++)
        {
            if (dataStream.Substring(i, 14).Distinct().Count() == 14)
            {
                return i+14;
            }
        }
        return 0;
    }
}