using System.Data.SqlTypes;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode;



public unsafe struct ProblemResult
{
    private readonly int _result;
    private fixed char _characters[100];
    private readonly int _length;
    public ProblemResult(int result)
    {
        _result = result;
    }
    public ProblemResult(ReadOnlySpan<char> result)
    {
        fixed (char* pChars = _characters)
        {
            result.CopyTo(new Span<char>(pChars, 100));
        }
        _length = Math.Min(result.Length, 100);
    }

    public override string ToString()
    {
        fixed (char* pChars = _characters)
        {
            return _length > 0 ? new string(pChars, 0, _length) : _result.ToString();
        }
    }

    public static implicit operator ProblemResult(int value) => new(value);
    public static implicit operator ProblemResult(string value) => new(value);

    public static bool operator==(ProblemResult p1, ProblemResult p2) => IsSame(p1, p2);

    private static bool IsSame(ProblemResult p1, ProblemResult p2)
    {
        if (p1._result != p2._result)
        {
            return false;
        }

        if (p1._length != p2._length)
        {
            return false;
        }

        if (p1._length > 0 && p2._length > 0)
        {
            return new ReadOnlySpan<char>(p1._characters, p1._length)
                .SequenceEqual(new ReadOnlySpan<char>(p2._characters, p2._length));
        }

        return true;
    }

    public static bool operator!=(ProblemResult p1, ProblemResult p2) => !IsSame(p1, p2);
}

internal interface IProblem
{
    static abstract int Id { get; }
    static abstract int Part { get; }
    static abstract ProblemResult Solve(ReadOnlySpan<byte> input);
    static abstract ProblemResult SolveNaive(ReadOnlySpan<string> input);
}