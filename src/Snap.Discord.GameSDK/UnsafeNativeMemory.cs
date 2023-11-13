using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

internal static class UnsafeNativeMemory
{
    public static unsafe T* Alloc<T>() where T : unmanaged
    {
        return (T*)NativeMemory.Alloc((nuint)sizeof(T));
    }
}