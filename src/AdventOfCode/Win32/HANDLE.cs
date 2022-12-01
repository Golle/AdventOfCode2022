using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Win32;

[StructLayout(LayoutKind.Sequential)]
public struct HANDLE
{
    public nuint Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nuint(HANDLE rawInput) => rawInput.Value;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nint(HANDLE rawInput) => (nint)rawInput.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HANDLE(nuint handle) => new() { Value = handle };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator HANDLE(nint handle) => new() { Value = (nuint)handle };
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool IsValid() => Value != unchecked((nuint)0xFFFFFFFFFFFFFFFFUL);

    public static HANDLE INVALID => new() { Value = unchecked((nuint)0xFFFFFFFFFFFFFFFFUL) };
}