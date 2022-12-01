using AdventOfCode;

if (!Win32FileSystem.Init())
{
    return -1;
}


var allocationsBefore = GC.GetTotalAllocatedBytes(true);
using var console = Win32Console.Create();

// problem 1 
{
    Span<byte> buff = stackalloc byte[1024];
    console.Write("This is a message\n"u8);

    var handle = Win32FileSystem.Open(@"../../../../../inputs/problem_1.txt"u8);
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

    
}

var allocationsAfter = GC.GetTotalAllocatedBytes(true);
if (allocationsAfter != allocationsBefore)
{
    Console.WriteLine($"{allocationsBefore} - {allocationsAfter}");
    return -1;
}

console.Write("YAY! NO ALLOCATIONS!"u8);
return 0;
