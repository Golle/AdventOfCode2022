using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode;
using AdventOfCode.Problems;

// THis will do a lot of allocations, change when there's time.
var problemPaths = Enumerable
    .Range(0, 100).Select(r => Encoding.UTF8.GetBytes(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../../../inputs/problem_{r}.txt"))))
    .ToArray();


if (!Win32FileSystem.Init())
{
    return -1;
}


PrintHeaders();
Run<Problem1Part1>(problemPaths);
Run<Problem1Part2>(problemPaths);
Run<Problem2Part1>(problemPaths);
Run<Problem2Part2>(problemPaths);
Run<Problem3Part1>(problemPaths);
Run<Problem3Part2>(problemPaths);
Run<Problem4Part1>(problemPaths);
Run<Problem4Part2>(problemPaths);
Run<Problem5Part1>(problemPaths);
Run<Problem5Part2>(problemPaths);

return 0;


static void Run<T>(byte[][] paths) where T : IProblem
{
    var allocationsBefore = GC.GetTotalAllocatedBytes(true);
    var result = SolveProblem<T>(paths);
    var allocationsAfter = GC.GetTotalAllocatedBytes(true);

    var naiveAllocationsBefore = GC.GetTotalAllocatedBytes(true);
    var resultNaive = SolveProblemNaive<T>(paths);
    var naiveAllocationsAfter = GC.GetTotalAllocatedBytes(true);
    PrintResult<T>(result, allocationsAfter - allocationsBefore, resultNaive, naiveAllocationsAfter - naiveAllocationsBefore);
}

[MethodImpl(MethodImplOptions.NoInlining)]
static ProblemResult SolveProblem<T>(byte[][] paths) where T : IProblem
{
    using var console = Win32Console.Create();
    var handle = Win32FileSystem.Open(paths[T.Id]);
    try
    {
        if (!handle.IsValid())
        {
            return -1;
        }
        var fileSize = Win32FileSystem.GetFileSize(handle);
        if (fileSize <= 0)
        {
            return -2;
        }

        Span<byte> fileBuffer = stackalloc byte[(int)fileSize];
        var byteRead = Win32FileSystem.Read(handle, fileBuffer);
        if (byteRead != fileBuffer.Length)
        {
            return -3;
        }
        return T.Solve(fileBuffer);
    }
    finally
    {
        Win32FileSystem.Close(handle);
    }
}

static ProblemResult SolveProblemNaive<T>(byte[][] paths) where T : IProblem
    => T.SolveNaive(File.ReadAllLines(Encoding.UTF8.GetString(paths[T.Id])));


static void PrintHeaders()
{
    Console.WriteLine($"{"Problem",-10}{"Result",10}{"Allocations(bytes)",25}{"Naive Result",20}{"Allocations(bytes)",25}{"Status",15}");
    Console.WriteLine();
}
static void PrintResult<T>(ProblemResult result, long allocations, ProblemResult naiveResult, long naiveAllocations) where T : IProblem
{
    Console.Write($"{$"{T.Id}:{T.Part}",-10}");
    Console.Write($"{result.ToString(),10}");
    Console.ForegroundColor = allocations != 0 ? ConsoleColor.Red : ConsoleColor.Green;
    Console.Write($"{allocations,25}");
    Console.ResetColor();
    Console.Write($"{naiveResult.ToString(),20}");
    Console.ForegroundColor = naiveAllocations != 0 ? ConsoleColor.Red : ConsoleColor.Green;
    Console.Write($"{naiveAllocations,25}");
    Console.ResetColor();

    var same = result == naiveResult;
    var status = same ? "O" : "X";
    Console.ForegroundColor = same ? ConsoleColor.Yellow: ConsoleColor.Red;
    Console.Write($"{status,13}");
    Console.ResetColor();
    Console.WriteLine();
}