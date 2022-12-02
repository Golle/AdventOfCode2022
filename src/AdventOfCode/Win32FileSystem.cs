using AdventOfCode.Win32;
using System.Runtime.InteropServices;
using System.Text;
using static AdventOfCode.Win32.CREATION_DISPOSITION;
using static AdventOfCode.Win32.FILE_ATTRIBUTE;
using static AdventOfCode.Win32.GENERIC_RIGHTS;

namespace AdventOfCode;

internal unsafe struct Win32FileSystem
{
    private static delegate*<byte*, uint, uint, SECURITY_ATTRIBUTES*, uint, uint, HANDLE, HANDLE> _createFileA;
    private static delegate*<HANDLE, void*, uint, uint*, OVERLAPPED*, int> _readFile;
    private static delegate*<HANDLE, void*, uint, uint*, OVERLAPPED*, int> _writeFile;
    private static delegate*<HANDLE, LARGE_INTEGER*, int> _getFileSize;
    private static delegate*<HANDLE, int> _closeHandle;

    public static bool Init()
    {
        var lib = NativeLibrary.Load("kernel32");
        if (lib == 0)
        {
            return false;
        }
        _createFileA = (delegate*<byte*, uint, uint, SECURITY_ATTRIBUTES*, uint, uint, HANDLE, HANDLE>)NativeLibrary.GetExport(lib, "CreateFileA");
        _readFile = (delegate*<HANDLE, void*, uint, uint*, OVERLAPPED*, int>)NativeLibrary.GetExport(lib, "ReadFile");
        _writeFile = (delegate*<HANDLE, void*, uint, uint*, OVERLAPPED*, int>)NativeLibrary.GetExport(lib, "WriteFile");
        _getFileSize = (delegate*<HANDLE, LARGE_INTEGER*, int>)NativeLibrary.GetExport(lib, "GetFileSizeEx");
        _closeHandle = (delegate*<HANDLE, int>)NativeLibrary.GetExport(lib, "CloseHandle");
        return true;
    }

    public static HANDLE Open(ReadOnlySpan<byte> path, bool writeAccess = false)
    {
        fixed (byte* pPath = path)
        {
            var access = writeAccess ? GENERIC_WRITE : GENERIC_READ;
            var handle = _createFileA(pPath, (uint)access, 0, null, (uint)OPEN_EXISTING, (uint)FILE_ATTRIBUTE_NORMAL, default);

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
            if (_readFile(handle, pBuffer, (uint)buffer.Length, &bytesRead, null) != 0)
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
            if (_writeFile(handle, pBuffer, (uint)buffer.Length, &bytesWritten, null) != 0)
            {
                return (int)bytesWritten;
            }

            return -1;
        }
    }

    public static long GetFileSize(HANDLE handle)
    {
        LARGE_INTEGER value;
        
        if (_getFileSize(handle, &value) != 0)
        {
            return (long)value.QuadPart;
        }
        return -1;
    }
    public static void Close(HANDLE handle) => _closeHandle(handle);
}

