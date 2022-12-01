using System.Runtime.InteropServices;

namespace AdventOfCode.Win32;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SECURITY_ATTRIBUTES
{
    public uint nLength;
    public void* lpSecurityDescriptor;
    public int bInheritHandle;
}