using System.Diagnostics;
using System.Text;
using AdventOfCode;

using var console = Win32Console.Create();

console.Write("Hello from non alloc AdventOfCode \n"u8);
var allocationsBefore = GC.GetTotalAllocatedBytes(true);
console.Write("Hello from non alloc AdventOfC123ode \n"u8);
// Problem  1
{
    const string ProblemPath = @"../../../../../inputs/problem_1.txt";
    Span<byte> buff = stackalloc byte[1024];

    const string Message = "This is a message\n";
    var length = Encoding.UTF8.GetBytes(Message, buff);
    console.Write(buff[..length]);

    //var handle = Win32FileSystem.Open(ProblemPath);
    //if (!handle.IsValid())
    //{
    //    return -1;
    //}

    //var fileSize = Win32FileSystem.GetFileSize(handle);
    //if (fileSize <= 0)
    //{
    //    return -2;
    //}

    //Span<byte> fileBuffer = stackalloc byte[(int)fileSize];
    //var byteRead = Win32FileSystem.Read(handle, fileBuffer);
    //if (byteRead != fileBuffer.Length)
    //{
    //    return -3;
    //}
}
var allocationsAfter = GC.GetTotalAllocatedBytes(true);
Debug.Assert(allocationsBefore == allocationsAfter);

return 0;