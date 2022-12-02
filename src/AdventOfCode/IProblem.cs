namespace AdventOfCode;

internal interface IProblem
{
    static abstract int Id { get; }
    static abstract int Part { get; }
    static abstract int Solve(ReadOnlySpan<byte> input);
    static abstract int SolveNaive(ReadOnlySpan<string> input);
}