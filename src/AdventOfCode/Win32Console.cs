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
        var handle = Win32FileSystem.Open("CON"u8, true);
        if (!handle.IsValid())
        {
            return default;
        }
        return new Win32Console(handle);
    }

    public void Write(ReadOnlySpan<byte> message)
    {
        Win32FileSystem.Write(_handle, message);
    }

    public void Dispose() => Win32FileSystem.Close(_handle);
}