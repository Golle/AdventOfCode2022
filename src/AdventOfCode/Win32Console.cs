using System.Diagnostics;
using AdventOfCode.Win32;

namespace AdventOfCode;

internal readonly struct Win32Console : IDisposable
{
    private readonly HANDLE _handle;
    private Win32Console(HANDLE handle)
    {
        _handle = handle;
    }

    public static Win32Console Create()
    {
        var handle = Win32FileSystem.Open("CON", true);
        if (!handle.IsValid())
        {
            return default;
        }

        return new Win32Console(handle);
    }

    public void Write(in ReadOnlySpan<byte> message)
    {
        //Debug.Assert(_handle.IsValid());
        var bytesWritten = Win32FileSystem.Write(_handle, message);
        //Debug.Assert(bytesWritten == message.Length);
    }

    public void Dispose() => Kernel32.CloseHandle(_handle);
}