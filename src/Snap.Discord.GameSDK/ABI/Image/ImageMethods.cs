using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Image;

namespace ABI.Snap.Discord.GameSDK.Image;

internal struct ImageMethods
{
    // void FetchCallback(IntPtr ptr, Result result, ImageHandle handleResult)
    // void FetchMethod(IntPtr methodsPtr, ImageHandle handle, bool refresh, IntPtr callbackData, FetchCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ImageMethods*, ImageHandle, bool, void*, delegate* unmanaged[Stdcall]<nint, Result, ImageHandle, void>, void> Fetch;

    // Result GetDimensionsMethod(IntPtr methodsPtr, ImageHandle handle, ref ImageDimensions dimensions)
    internal unsafe delegate* unmanaged[Stdcall]<ImageMethods*, ImageHandle, ImageDimensions*, Result> GetDimensions;

    // Result GetDataMethod(IntPtr methodsPtr, ImageHandle handle, byte[] data, Int32 dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<ImageMethods*, ImageHandle, byte*, int, Result> GetData;
}
