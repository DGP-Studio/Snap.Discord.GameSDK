using ABI.Snap.Discord.GameSDK.Image;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK.Image;

public class ImageManager
{
    private unsafe readonly ImageMethods* MethodsPtr;

    internal unsafe ImageManager(ImageMethods* ptr, nint eventsPtr, ref ImageEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(nint eventsPtr, ref ImageEvents events)
    {
        *(ImageEvents*)eventsPtr = events;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void FetchCallbackImpl(nint ptr, Result result, ImageHandle handleResult)
    {
        // void FetchHandler(Result result, ImageHandle handleResult)
        ((delegate* unmanaged[Stdcall]<Result, ImageHandle, void>)ptr)(result, handleResult);
    }

    public unsafe void Fetch(ImageHandle handle, bool refresh, delegate* unmanaged[Stdcall]<Result, ImageHandle, void> callback)
    {
        MethodsPtr->Fetch(MethodsPtr, handle, refresh, callback, &FetchCallbackImpl);
    }

    public unsafe ImageDimensions GetDimensions(ImageHandle handle)
    {
        ImageDimensions ret = default;
        Result res = MethodsPtr->GetDimensions(MethodsPtr, handle, &ret);
        ResultException.ThrowOnFailure(res);
        return ret;
    }

    public unsafe void GetData(ImageHandle handle, Span<byte> data)
    {
        fixed (byte* pData = data)
        {
            Result res = MethodsPtr->GetData(MethodsPtr, handle, pData, data.Length);
            ResultException.ThrowOnFailure(res);
        }
    }
}