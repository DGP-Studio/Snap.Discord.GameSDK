using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class ImageManager
{
    private unsafe readonly ImageMethods* MethodsPtr;

    internal unsafe ImageManager(ImageMethods* ptr, ImageEvents* eventsPtr, ref ImageEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(ImageEvents* eventsPtr, ref ImageEvents events)
    {
        *eventsPtr = events;
    }

    public unsafe void Fetch(ImageHandle handle, bool refresh, FetchHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void FetchCallbackImpl(FetchHandler ptr, Result result, ImageHandle handleResult)
        {
            ptr.Invoke(result, handleResult);
        }

        MethodsPtr->Fetch.Invoke(MethodsPtr, handle, refresh, callback, FetchCallback.Create(&FetchCallbackImpl));
    }

    public unsafe ImageDimensions GetDimensions(ImageHandle handle)
    {
        ImageDimensions ret = default;
        MethodsPtr->GetDimensions.Invoke(MethodsPtr, handle, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe void GetData(ImageHandle handle, Span<byte> data)
    {
        fixed (byte* pData = data)
        {
            MethodsPtr->GetData.Invoke(MethodsPtr, handle, pData, data.Length).ThrowOnFailure();
        }
    }
}