using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;

namespace Snap.Discord.GameSDK;

internal static class ByteString
{
    public static unsafe string Get(byte* ptr)
    {
        return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
    }

    public static unsafe void Set(byte* ptr, int length, string value)
    {
        Span<byte> target = new(ptr, length);
        target.Clear();
        Utf8.FromUtf16(value, target, out _, out _);
    }
}