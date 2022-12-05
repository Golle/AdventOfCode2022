﻿using AdventOfCode.Tokens;
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
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        var cursor = new Cursor(input);
        var count = 0;
        do
        {
            var pair1 = ReadPair(ref cursor);
            var separator = cursor.ReadNextToken();
            Debug.Assert(separator.Type is TokenType.Comma);
            var pair2 = ReadPair(ref cursor);
            var end = cursor.ReadNextToken();
            Debug.Assert(end.Type is TokenType.NewLine or TokenType.Invalid);
            if (pair1.Overlaps(pair2))
            {
                count++;
            }

        } while (cursor.HasMore);

        return count;

        static Pair ReadPair(ref Cursor cursor)
        {
            var start = cursor.ReadNextToken().AsInteger();
            var dash = cursor.ReadNextToken();
            Debug.Assert(dash.Type == TokenType.Dash);
            var end = cursor.ReadNextToken().AsInteger();
            return new Pair(start, end);
        }
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
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
                count++;
            }
        }

        return count;
    }
}