using AdventOfCode.Win32;
using static AdventOfCode.Win32.CREATION_DISPOSITION;
using static AdventOfCode.Win32.FILE_ATTRIBUTE;
using static AdventOfCode.Win32.GENERIC_RIGHTS;
using static AdventOfCode.Win32.Kernel32;

namespace AdventOfCode;

internal unsafe struct Win32FileSystem
{
    public static HANDLE Open(ReadOnlySpan<char> path, bool writeAccess = false)
    {
        fixed (char* pPath = path)
        {
            var access = writeAccess ? GENERIC_WRITE : GENERIC_READ;
            var handle = CreateFileW(pPath, (uint)access, 0, null, (uint)OPEN_EXISTING, (uint)FILE_ATTRIBUTE_NORMAL, default);

            if (handle.IsValid())
            {
                return handle;
            }
        }
        return HANDLE.INVALID;
    }

    public static int Read(HANDLE handle, Span<byte> buffer)
    {
        fixed (byte* pBuffer = buffer)
        {
            uint bytesRead = 0;
            if (ReadFile(handle, pBuffer, (uint)buffer.Length, &bytesRead, null))
            {
                return (int)bytesRead;
            }
            return -1;
        }
    }

    public static int Write(HANDLE handle, ReadOnlySpan<byte> buffer)
    {
        fixed (byte* pBuffer = buffer)
        {
            uint bytesWritten = 0;
            if (WriteFile(handle, pBuffer, (uint)buffer.Length, &bytesWritten, null))
            {
                return (int)bytesWritten;
            }

            return -1;
        }
    }

    public static long GetFileSize(HANDLE handle)
    {
        LARGE_INTEGER value;
        if (GetFileSizeEx(handle, &value))
        {
            return (long)value.QuadPart;
        }
        return -1;
    }
    public static void Close(HANDLE handle) => CloseHandle(handle);
}

