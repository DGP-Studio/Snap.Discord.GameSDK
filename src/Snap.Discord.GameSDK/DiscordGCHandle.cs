using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

internal static class DiscordGCHandle
{
    public static unsafe nint Alloc<T>(Discord obj)
    {
        GCHandle handle = GCHandle.Alloc(obj);
        return GCHandle.ToIntPtr(handle);
    }

    public static unsafe void Free(nint ptr)
    {
        GCHandle handle = GCHandle.FromIntPtr(ptr);
        handle.Free();
    }

    public static unsafe Discord Get(nint ptr)
    {
        GCHandle handle = GCHandle.FromIntPtr(ptr);
        return (Discord)handle.Target!;
    }
}