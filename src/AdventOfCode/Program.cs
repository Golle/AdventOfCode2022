using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode;

// THis will do a lot of allocations, change when there's time.
var problemPaths = Enumerable
    .Range(0, 100).Select(r => Encoding.UTF8.GetBytes(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../../../inputs/problem_{r}.txt"))))
    .ToArray();


if (!Win32FileSystem.Init())
{
    return -1;
}

Run<Problem1>(problemPaths);
Run<Problem2>(problemPaths);

return 0;


static void Run<T>(byte[][] paths) where T : IProblem
{
    var allocationsBefore = GC.GetTotalAllocatedBytes(true);
    var result = SolveProblem<T>(paths);
    var allocationsAfter = GC.GetTotalAllocatedBytes(true);

    var resultNaive = SolveProblemNaive<T>(paths);
    Console.WriteLine($"Problem {T.Id} result: {result}. (NaiveResult: {resultNaive})");
    if (allocationsAfter != allocationsBefore)
    {
        Console.WriteLine($"Allocation check for problem {T.Id} failed. (Before: {allocationsBefore} After: {allocationsAfter})");
    }
}

[MethodImpl(MethodImplOptions.NoInlining)]
static int SolveProblem<T>(byte[][] paths) where T : IProblem
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

static int SolveProblemNaive<T>(byte[][] paths) where T : IProblem
    => T.SolveNaive(File.ReadAllLines(Encoding.UTF8.GetString(paths[T.Id])));