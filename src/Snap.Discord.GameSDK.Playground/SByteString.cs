using System.Runtime.InteropServices;
using System.Text.Unicode;

namespace Snap.Discord.GameSDK.Playground;

internal static class SByteString
{
    public static unsafe void Set(sbyte* reference, int length, string source)
    {
        Span<sbyte> sbytes = new(reference, length);
        sbytes.Clear();
        Utf8.FromUtf16(source.AsSpan(), MemoryMarshal.Cast<sbyte, byte>(sbytes), out _, out _);
    }
}